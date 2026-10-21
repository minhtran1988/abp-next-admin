using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using Xunit;

namespace LINGYUN.Abp.Rules.RulesEngine
{
    public class TestInputRuleTests : AbpRulesEngineTestBase
    {
        private readonly IRuleProvider _ruleProvider;

        public TestInputRuleTests()
        {
            _ruleProvider = GetRequiredService<IRuleProvider>();
        }

        [Fact]
        public async Task Input_Required_Should_Required()
        {
            var input = new TestInput 
            {
                Integer1 = 101,
                Integer2 = 99,
                Length = "123456"
            }
            ;
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _ruleProvider.ExecuteAsync(input);
            });

            exception.Message.ShouldBe("One or more rules failed");
            exception.ValidationErrors.Count.ShouldBe(1);
            exception.ValidationErrors[0].ErrorMessage.ShouldBe("Field Required must be entered!");
        }

        [Fact]
        public async Task Input_Integer1_Should_MustBeGreaterThan100()
        {
            var input = new TestInput
            {
                Required = "123456",
                Integer1 = 99,
                Integer2 = 99,
                Length = "123456"
            };
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _ruleProvider.ExecuteAsync(input);
            });

            exception.Message.ShouldBe("One or more rules failed");
            exception.ValidationErrors.Count.ShouldBe(1);
            exception.ValidationErrors[0].ErrorMessage.ShouldBe("Field Integer1 must be greater than 100!");
        }

        [Fact]
        public async Task Input_Integer2_Should_MustBeLessThan100()
        {
            var input = new TestInput
            {
                Required = "123456",
                Integer1 = 101,
                Integer2 = 100,
                Length = "123456"
            };
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _ruleProvider.ExecuteAsync(input);
            });

            exception.Message.ShouldBe("One or more rules failed");
            exception.ValidationErrors.Count.ShouldBe(1);
            exception.ValidationErrors[0].ErrorMessage.ShouldBe("Field Integer2 must be less than 100!");
        }

        [Fact]
        public async Task Input_Sum_Integer1_And_Integer2_Should_MustBeGreaterThan150()
        {
            var input = new TestInput
            {
                Required = "1",
                Integer1 = 101,
                Integer2 = 48,
                Length = "123456"
            };
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _ruleProvider.ExecuteAsync(input);
            });

            exception.Message.ShouldBe("One or more rules failed");
            exception.ValidationErrors.Count.ShouldBe(1);
            exception.ValidationErrors[0].ErrorMessage.ShouldBe("The sum of fields Integer1 and Integer2 must be greater than 150!");
        }

        [Fact]
        public async Task Input_Required_Length_Should_MustBeGreaterThan5()
        {
            var input = new TestInput
            {
                Required = "1",
                Integer1 = 101,
                Integer2 = 50,
                Length = "12345"
            };
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _ruleProvider.ExecuteAsync(input);
            });

            exception.Message.ShouldBe("One or more rules failed");
            exception.ValidationErrors.Count.ShouldBe(1);
            exception.ValidationErrors[0].ErrorMessage.ShouldBe("Field Length length must be greater than 5!");
        }
    }
}
