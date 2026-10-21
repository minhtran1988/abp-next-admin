using LINGYUN.Abp.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.Notifications.ChannelData.Tests;

/// <summary>
/// Channel data (load) framework contract test
/// </summary>
/// <remarks>
/// <para>
/// This document only tests the **framework contract** itself: weak type conversion, existence determination, registration period verification,
/// Prioritization, JSON round trip. The test load is the stub type defined in this file.
/// </para>
/// <para>
/// **Specific channel** data testing is in the respective test projects (first new message object →
/// <c>IJsonSerializer</c> serialization → channel side verification):
/// <list type="bullet">
/// <item><c>LINGYUN.Abp.Notifications.WeChat.Work.Tests</c></item>
/// <item><c>LINGYUN.Abp.Notifications.Webhook.WeChat.Work.Tests</c></item>
/// <item><c>LINGYUN.Abp.Notifications.WeChat.MiniProgram.Tests</c></item>
/// <item><c>LINGYUN.Abp.Notifications.WxPusher.Tests</c></item>
/// <item><c>LINGYUN.Abp.Notifications.PushPlus.Tests</c></item>
/// </list>
/// </para>
/// </remarks>
public class NotificationChannelData_Tests
{
    // ---------- 1. Weak type conversion ----------

    [Theory]
    [InlineData((byte)1)]
    [InlineData(1)]
    [InlineData(1L)]
    [InlineData("1")]
    [InlineData("\"1\"")]
    [InlineData(true)]
    [InlineData("true")]
    public void Should_Coerce_All_Representations_To_Bool(object stored)
    {
        var data = new NotificationData();
        data.TrySetData("key", stored);

        CreateResolver().GetValue(data, "key", false).ShouldBeTrue();
    }

    [Fact]
    public void Should_Distinguish_Unset_From_Explicit_False()
    {
        var unset = new NotificationData();
        CreateResolver().GetValue<bool>(unset, "key").ShouldBeNull();

        var explicitFalse = new NotificationData();
        explicitFalse.TrySetData("key", 0);
        CreateResolver().GetValue<bool>(explicitFalse, "key").ShouldBe(false);
    }

    [Fact]
    public void Should_Read_Values_After_Json_RoundTrip()
    {
        // Notification data must undergo JSON round-trip after persistence/failure retry/cross-process delivery
        var data = new NotificationData();
        data.TrySetData("key", (byte)1);

        var roundTripped = RoundTrip(data);
        roundTripped.ExtraProperties["key"].ShouldNotBeOfType<byte>();

        CreateResolver().GetValue(roundTripped, "key", false).ShouldBeTrue();
    }

    [Fact]
    public void Should_Match_Keys_Case_Insensitively()
    {
        var data = new NotificationData();
        data.TrySetData("my-stub:key", "value-1");

        var resolver = CreateResolver();

        // accurate hit
        resolver.GetOrNull(data, "my-stub:key").ShouldBe("value-1");
        // Any case should hit
        resolver.GetOrNull(data, "MY-STUB:KEY").ShouldBe("value-1");
        resolver.GetOrNull(data, "My-Stub:Key").ShouldBe("value-1");
    }

    // ---------- 2. Existence determination (declarative) ----------

    [Fact]
    public void Should_Detect_Explicit_Payload()
    {
        var data = new NotificationData();
        ChannelData().Write(data, new PriorityStubA { A = "a" });

        CreateResolver().IsDeclared(data, typeof(PriorityStubA)).ShouldBeTrue();
        CreateResolver().IsDeclared(data, typeof(PriorityStubB)).ShouldBeFalse();
    }

    [Fact]
    public void Should_Not_Treat_Ignored_Properties_As_Payload()
    {
        var data = new NotificationData();
        // Write-only properties marked "read-only": shall not be considered to declare a payload
        data.TrySetData("stubB:optional", "value");

        CreateResolver().IsDeclared(data, typeof(PriorityStubB)).ShouldBeFalse();
    }

    [Fact]
    public void Should_Apply_AnyDeclaredProperty_Presence_Mode()
    {
        var resolver = CreateResolver();

        var first = new NotificationData();
        first.TrySetData("stub-many:first", "v");
        resolver.IsDeclared(first, typeof(AnyDeclaredPropertyStub)).ShouldBeTrue();

        var second = new NotificationData();
        second.TrySetData("stub-many:second", "v");
        resolver.IsDeclared(second, typeof(AnyDeclaredPropertyStub)).ShouldBeTrue();

        // The key determination is declared, but not a single key is written -> does not exist
        resolver.IsDeclared(new NotificationData(), typeof(AnyDeclaredPropertyStub)).ShouldBeFalse();
    }

    [Fact]
    public void Should_Ignore_Matching_For_Optional_Properties()
    {
        var data = new NotificationData();
        // Only write "read-only" properties -> not counted as payload
        data.TrySetData("stubB:optional", "value");
        CreateResolver().IsDeclared(data, typeof(PriorityStubB)).ShouldBeFalse();

        // Write identification key -> Calculate load
        var withIdentity = new NotificationData();
        withIdentity.TrySetData("stub:b", "value");
        CreateResolver().IsDeclared(withIdentity, typeof(PriorityStubB)).ShouldBeTrue();
    }

    [Fact]
    public void Should_Allow_Always_Present_Payload()
    {
        var resolver = CreateResolver();

        // Presence = None: unconditionally exists
        resolver.IsDeclared(new NotificationData(), typeof(AlwaysPresentStub)).ShouldBeTrue();

        var payload = resolver.Resolve<AlwaysPresentStub>(new NotificationData());
        payload.ShouldNotBeNull();
    }

    // ---------- 3. Registration period verification ----------

    [Fact]
    public void Should_Throw_When_Presence_Declared_Without_Matchable_Property()
    {
        var exception = Should.Throw<AbpNotificationChannelDataConfigurationException>(
            () => CreateOptions().Add<MissingMatchablePropertyStub>());

        exception.Message.ShouldContain("matchable property");
    }

    [Fact]
    public void Should_Reject_Missing_Channel_Data_Attribute()
    {
        var exception = Should.Throw<AbpNotificationChannelDataConfigurationException>(
            () => CreateOptions().Add<NoAttributeStub>());

        exception.Message.ShouldContain("NotificationChannelDataAttribute");
    }

    [Fact]
    public void Should_Reject_Duplicate_Priority_In_Same_Provider()
    {
        var options = CreateOptions();
        options.Add<PriorityStubA>();

        var exception = Should.Throw<AbpNotificationChannelDataConfigurationException>(
            () => options.Add<DuplicatePriorityStub>());

        exception.Message.ShouldContain("same priority");
    }

    // ---------- 4. Parsing and sorting ----------

    [Fact]
    public void Should_Resolve_Payloads_In_Declared_Priority_Order()
    {
        var data = new NotificationData();
        ChannelData().Write(data, new PriorityStubB { B = "b" });
        ChannelData().Write(data, new PriorityStubA { A = "a" });

        var resolved = CreateResolver().Resolve(TestProvider, data);

        // Only A and B are declared; AnyDeclaredPropertyStub is not written, AlwaysPresentStub(Presence=None)
        // Exists unconditionally, so there are 3 in total
        resolved.Count.ShouldBe(3);
        // Priority: AlwaysPresentStub=400, A=100, B=200 -> After sorting A, B, Always
        resolved[0].ShouldBeOfType<PriorityStubA>();
        resolved[1].ShouldBeOfType<PriorityStubB>();
        resolved[2].ShouldBeOfType<AlwaysPresentStub>();
    }

    [Fact]
    public void Should_Resolve_Payload_After_Json_RoundTrip()
    {
        var data = new NotificationData();
        ChannelData().Write(data, new PriorityStubA { A = "Card Title" });

        // Complex payloads are stored in the form of JSON strings and must still be able to be restored to strong types after round-trips
        data.ExtraProperties["stub:a"].ShouldBeOfType<string>();

        var payload = CreateResolver().Resolve<PriorityStubA>(RoundTrip(data));

        payload.ShouldNotBeNull();
        payload!.A.ShouldBe("Card Title");
    }

    [Fact]
    public void Should_Not_Throw_For_Invalid_Payload_But_Report_Declared()
    {
        var data = new NotificationData();
        // Complex payloads are stored as JSON strings; illegal JSON is written here
        data.TrySetData("stub:json", "{ this is not json }");

        var resolver = CreateResolver();

        // IsDeclared is still true (the producer did write), Resolve returns null (resolution failed, only logged)
        resolver.IsDeclared(data, typeof(JsonPayloadStub)).ShouldBeTrue();
        resolver.Resolve<JsonPayloadStub>(data).ShouldBeNull();
        // Diagnostic information gives the actual key name hit
        resolver.GetKey(data, typeof(JsonPayloadStub)).ShouldBe("stub:json");
    }

    [Fact]
    public void Should_Resolve_Complex_Payload_After_Json_RoundTrip()
    {
        var data = new NotificationData();
        ChannelData().Write(data, new JsonPayloadStub
        {
            Payload = new StubPayload { Name = "Title", Count = 3 }
        });

        // Complex types are stored as JSON strings
        data.ExtraProperties["stub:json"].ShouldBeOfType<string>();

        var payload = CreateResolver().Resolve<JsonPayloadStub>(RoundTrip(data));

        payload.ShouldNotBeNull();
        payload!.Payload.ShouldNotBeNull();
        payload.Payload!.Name.ShouldBe("Title");
        payload.Payload.Count.ShouldBe(3);
    }

    // ---------- 5. Concurrent behavior of attribute metadata cache ----------

    /// <remarks>
    /// Each payload on the delivery path will read the attribute metadata cache, and the first build of the cache requires reflection and
    /// Expression compilation, so "it is only built once under high concurrency and the read path is lock-free" is a property that must be guaranteed.
    /// </remarks>
    [Fact]
    public void Should_Build_Property_Metadata_Once_Under_Concurrency()
    {
        var payloadTypes = new[]
        {
            typeof(JsonPayloadStub),
            typeof(PriorityStubA),
            typeof(PriorityStubB),
            typeof(AnyDeclaredPropertyStub),
            typeof(AlwaysPresentStub)
        };

        // Each type is fetched multiple times concurrently, covering the competition of "multiple threads constructing the same type for the first time at the same time"
        var results = new NotificationChannelDataProperty[payloadTypes.Length * 64][];

        Parallel.For(0, results.Length, index =>
        {
            results[index] = NotificationChannelDataPropertyCache.GetProperties(
                payloadTypes[index % payloadTypes.Length]);
        });

        foreach (var payloadType in payloadTypes)
        {
            var expected = NotificationChannelDataPropertyCache.GetProperties(payloadType);

            var observed = results
                .Where(x => x.Length == expected.Length)
                .Where(x => x.Select(p => p.Key).SequenceEqual(expected.Select(p => p.Key)))
                .ToArray();

            // All concurrent calls must see the same metadata (same instance), proving that it was only built once
            observed.ShouldNotBeEmpty();
            observed.ShouldAllBe(x => ReferenceEquals(x, expected));
        }
    }

    [Fact]
    public void Should_Return_Same_Instance_On_Repeated_Calls()
    {
        var first = NotificationChannelDataPropertyCache.GetProperties(typeof(JsonPayloadStub));
        var second = NotificationChannelDataPropertyCache.GetProperties(typeof(JsonPayloadStub));

        ReferenceEquals(first, second).ShouldBeTrue();
    }

    // ---------- 6. Test load ----------

    private const string TestProvider = "stub";

    [NotificationChannelData(TestProvider, Priority = 100)]
    private class PriorityStubA : NotificationChannelData
    {
        [NotificationChannelProperty("stub:a")]
        public string? A { get; set; }
    }

    [NotificationChannelData(TestProvider, Priority = 200)]
    private class PriorityStubB : NotificationChannelData
    {
        [NotificationChannelProperty("stub:b")]
        public string? B { get; set; }

        [NotificationChannelProperty("stubB:optional", IgnoreWhenMatching = true)]
        public string? Optional { get; set; }
    }

    [NotificationChannelData(TestProvider, Priority = 100)]
    private class DuplicatePriorityStub : NotificationChannelData
    {
        [NotificationChannelProperty("stub:dup")]
        public string? Value { get; set; }
    }

    [NotificationChannelData(TestProvider, Priority = 300,
        Presence = NotificationChannelDataPresenceMode.AnyDeclaredProperty)]
    private class AnyDeclaredPropertyStub : NotificationChannelData
    {
        [NotificationChannelProperty("stub-many:first")]
        public string? First { get; set; }

        [NotificationChannelProperty("stub-many:second")]
        public string? Second { get; set; }
    }

    [NotificationChannelData(TestProvider, Priority = 400,
        Presence = NotificationChannelDataPresenceMode.None)]
    private class AlwaysPresentStub : NotificationChannelData
    {
        [NotificationChannelProperty("stub-always:value")]
        public string? Value { get; set; }
    }

    [NotificationChannelData(TestProvider, Priority = 500,
        Presence = NotificationChannelDataPresenceMode.DeclaredProperties)]
    private class MissingMatchablePropertyStub : NotificationChannelData
    {
        // There is no property with [NotificationChannelProperty]
        public string? Value { get; set; }
    }

    /// <summary>
    /// Complex payload (stored as JSON string)
    /// </summary>
    [NotificationChannelData(TestProvider, Priority = 600)]
    private class JsonPayloadStub : NotificationChannelData
    {
        [NotificationChannelProperty("stub:json")]
        public StubPayload? Payload { get; set; }
    }

    private class StubPayload
    {
        public string? Name { get; set; }

        public int Count { get; set; }
    }

    private class NoAttributeStub : NotificationChannelData
    {
        public string? Value { get; set; }
    }

    // ---------- Auxiliary ----------

    private static AbpNotificationChannelDataOptions CreateOptions()
    {
        // The option is pure data: the existence determination is purely static, and there is no need to parse any services during registration.
        return new AbpNotificationChannelDataOptions();
    }

    private static NotificationChannelDataResolver CreateResolver()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<INotificationChannelDataJsonSerializer,
            TestNotificationChannelDataJsonSerializer>();
        services.AddSingleton<INotificationChannelDataAccessor, NotificationChannelDataAccessor>();
        services.AddSingleton<INotificationChannelDataPresenceProvider, NotificationChannelDataPresenceProvider>();
        services.AddSingleton<INotificationChannelDataResolver, NotificationChannelDataResolver>();

        services.PostConfigure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<PriorityStubA>();
            options.Add<PriorityStubB>();
            options.Add<AnyDeclaredPropertyStub>();
            options.Add<AlwaysPresentStub>();
            options.Add<JsonPayloadStub>();
        });

        var provider = services.BuildServiceProvider();

        return (NotificationChannelDataResolver)provider.GetRequiredService<INotificationChannelDataResolver>();
    }

    private static INotificationChannelDataSerializer ChannelData()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<INotificationChannelDataJsonSerializer,
            TestNotificationChannelDataJsonSerializer>();
        services.AddSingleton<INotificationChannelDataAccessor, NotificationChannelDataAccessor>();
        services.AddSingleton<INotificationChannelDataPresenceProvider, NotificationChannelDataPresenceProvider>();
        services.AddSingleton<INotificationChannelDataSerializer, NotificationChannelDataSerializer>();

        return services.BuildServiceProvider().GetRequiredService<INotificationChannelDataSerializer>();
    }

    private static NotificationData RoundTrip(NotificationData data)
    {
        var json = JsonSerializer.Serialize(data.ExtraProperties);

        return new NotificationData
        {
            ExtraProperties = JsonSerializer.Deserialize<Volo.Abp.Data.ExtraPropertyDictionary>(json)!
        };
    }
}
