using LINGYUN.Abp.Authorization.OrganizationUnits;
using LINGYUN.Abp.Authorization.Permissions;
using LINGYUN.Abp.DataProtection.Keywords;
using LINGYUN.Abp.DataProtection.Stores;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Security.Claims;
using Xunit;

namespace LINGYUN.Abp.DataProtection
{
    public class ProtectionFieldTests : AbpDataProtectionTestBase
    {
        private readonly IFakeProtectionObjectRepository _repository;
        private readonly ICurrentPrincipalAccessor _accessor;
        private readonly IDataProtectedResourceStore _store;
        private readonly IDataFilter _dataFilter;

        public ProtectionFieldTests()
        {
            _repository = GetRequiredService<IFakeProtectionObjectRepository>();
            _accessor = GetRequiredService<ICurrentPrincipalAccessor>();
            _store = GetRequiredService<IDataProtectedResourceStore>();
            _dataFilter = GetRequiredService<IDataFilter>();
        }

        [Fact]
        public async virtual Task Should_Protected_Data_By_Repo()
        {
            var validUser = Guid.NewGuid();

            await WithUnitOfWorkAsync(async () =>
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(AbpClaimTypes.UserId, validUser.ToString()));
                identity.AddClaim(new Claim(AbpClaimTypes.UserName, "user1"));
                identity.AddClaim(new Claim(AbpClaimTypes.Role, "role1"));
                identity.AddClaim(new Claim(AbpClaimTypes.Role, "role2"));
                identity.AddClaim(new Claim(AbpOrganizationUnitClaimTypes.OrganizationUnit, "00001"));
                identity.AddClaim(new Claim(AbpOrganizationUnitClaimTypes.OrganizationUnit, "00001.00002"));
                using (_accessor.Change(new ClaimsPrincipal(identity)))
                {
                    var values = new List<FakeProtectionObject>()
                    {
                        new FakeProtectionObject(1)
                        {
                            Protect1 = "Protect1",
                            Protect2 = "Protect1",
                            Value1 = "Value1",
                            Value2 = "Value1",
                            ProtectNum1 = 100,
                            ProtectNum2 = 200,
                            Num3 = 400,
                        },
                        new FakeProtectionObject(2)
                        {
                            Protect1 = "test",
                            Protect2 = "Protect2",
                            Value1 = "Value2",
                            Value2 = "Value2",
                            ProtectNum1 = 1000,
                            ProtectNum2 = 2000,
                            Num3 = 3000
                        },
                        new FakeProtectionObject(3)
                        {
                            Protect1 = "test1",
                            Protect2 = "Protect3",
                            Value1 = "Value3",
                            Value2 = "Value3",
                            ProtectNum1 = 10000,
                            ProtectNum2 = 20000,
                            Num3 = 300
                        },
                        new FakeProtectionObject(4)
                        {
                            Protect1 = "test3",
                            Protect2 = "Protect4",
                            Value1 = "Value4",
                            Value2 = "Value4",
                            ProtectNum1 = 10000,
                            ProtectNum2 = 20000,
                            Num3 = 300
                        }
                    };
                    await _repository.InsertManyAsync(values, true);
                }

                // role2 rules
                var rule2FilterGroup = new DataAccessFilterGroup();
                // Only allowed to query Num3 less than or equal to 400
                rule2FilterGroup.AddRule(new DataAccessFilterRule(nameof(FakeProtectionObject.Num3), 400, typeof(int).FullName, "number", DataAccessFilterOperate.LessOrEqual));
                await _store.SetAsync(new DataAccessResource(RolePermissionValueProvider.ProviderName, "role2", typeof(FakeProtectionObject).FullName, DataAccessOperation.Read, rule2FilterGroup));

                // role3 editing rules
                var rule3WriteAccess = new DataAccessFilterGroup();
                // Only allowed to edit the data submitted by yourself
                rule3WriteAccess.AddRule(new DataAccessFilterRule(nameof(FakeProtectionObject.CreatorId), DataAccessCurrentUserContributor.Name, typeof(Guid?).FullName, "string", DataAccessFilterOperate.Equal));
                var rule3WriteAccessCacheItem = new DataAccessResource(RolePermissionValueProvider.ProviderName, "role3", typeof(FakeProtectionObject).FullName, DataAccessOperation.Write, rule3WriteAccess);
                // Only allow editing of Num3 fields
                rule3WriteAccessCacheItem.AccessedProperties.AddRange(new string[] { nameof(FakeProtectionObject.Num3) });
                await _store.SetAsync(rule3WriteAccessCacheItem);

                // role1 reading rules
                var rule1ReadAccess = new DataAccessFilterGroup();
                // Only allowed to read the data submitted by yourself
                rule1ReadAccess.AddRule(new DataAccessFilterRule(nameof(FakeProtectionObject.CreatorId), DataAccessCurrentUserContributor.Name, typeof(Guid?).FullName, "string", DataAccessFilterOperate.Equal));
                var rule1ReadAccessCacheItem = new DataAccessResource(RolePermissionValueProvider.ProviderName, "role1", typeof(FakeProtectionObject).FullName, DataAccessOperation.Read, rule1ReadAccess);
                // Only allowed to read Num3 field
                rule1ReadAccessCacheItem.AccessedProperties.AddRange(new string[] { nameof(FakeProtectionObject.Id), nameof(FakeProtectionObject.Num3) });
                await _store.SetAsync(rule1ReadAccessCacheItem);


                // ou1 reading rules
                var ou1ReadAccess = new DataAccessFilterGroup();
                // Allow reading data of this department and subordinate departments
                // Get the department tree structure list to facilitate adding multiple department condition sets
                ou1ReadAccess.AddRule(new DataAccessFilterRule($"{nameof(FakeProtectionObject.CreatorId)}", new List<Guid?>() { validUser, Guid.NewGuid() }, typeof(Guid?).FullName, "string", DataAccessFilterOperate.Contains, true));
                // ou1ReadAccess.AddRule(new DataAccessFilterRule($"{nameof(FakeProtectionObject.ExtraProperties)}.{DataAccessKeywords.AUTH_ORGS}", "[00001]", DataAccessFilterOperate.Contains));
                // ou1ReadAccess.AddRule(new DataAccessFilterRule($"{nameof(FakeProtectionObject.ExtraProperties)}.{DataAccessKeywords.AUTH_ORGS}", "[00001.00002]", DataAccessFilterOperate.Contains));

                var ou1ReadAccessCacheItem = new DataAccessResource(OrganizationUnitPermissionValueProvider.ProviderName, "00001", typeof(FakeProtectionObject).FullName, DataAccessOperation.Read, ou1ReadAccess);

                // Only allowed to read Num3 field
                ou1ReadAccessCacheItem.AccessedProperties.AddRange(new string[] { nameof(FakeProtectionObject.Id), nameof(FakeProtectionObject.Num3) });
                await _store.SetAsync(ou1ReadAccessCacheItem);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                // Marking without enabling the data filter should be able to get all the data
                var result = await _repository.GetAllListAsync();
                result.Count.ShouldBe(4);
            });

            var readUser = Guid.NewGuid();
            await WithUnitOfWorkAsync(async () =>
            {
                var identity2 = new ClaimsIdentity();
                identity2.AddClaim(new Claim(AbpClaimTypes.UserId, readUser.ToString()));
                identity2.AddClaim(new Claim(AbpClaimTypes.UserName, "user2"));
                identity2.AddClaim(new Claim(AbpClaimTypes.Role, "role2"));
                using (_accessor.Change(identity2))
                {
                    // Should only be able to read data with Num3 less than or equal to 400, only 3 items
                    var result = await _repository.GetListAsync();
                    result.Count.ShouldBe(3);
                }
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var identity3 = new ClaimsIdentity();
                identity3.AddClaim(new Claim(AbpClaimTypes.UserId, readUser.ToString()));
                identity3.AddClaim(new Claim(AbpClaimTypes.UserName, "user2"));
                identity3.AddClaim(new Claim(AbpOrganizationUnitClaimTypes.OrganizationUnit, "00001"));
                using (_accessor.Change(identity3))
                {
                    // Should be able to get 4 pieces of data
                    var result = await _repository.GetListAsync();
                    result.Count.ShouldBe(4);
                }
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var identity3 = new ClaimsIdentity();
                identity3.AddClaim(new Claim(AbpClaimTypes.UserId, validUser.ToString()));
                identity3.AddClaim(new Claim(AbpClaimTypes.UserName, "user3"));
                identity3.AddClaim(new Claim(AbpClaimTypes.Role, "role3"));
                using (_accessor.Change(identity3))
                {
                    var entity = await _repository.FindAsync(1);
                    entity.Protect1 = "New Protect1";
                    entity.Num3 = 100;
                    // Switching to an authorized user should be able to save data
                    var resultEntity = await _repository.UpdateAsync(entity, true);
                    // The authorization field should be saved successfully
                    resultEntity.Num3.ShouldBe(100);
                    // Unauthorized fields should not be saved successfully
                    resultEntity.Protect1.ShouldBe("Protect1");
                }
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var identity3 = new ClaimsIdentity();
                identity3.AddClaim(new Claim(AbpClaimTypes.UserId, validUser.ToString()));
                identity3.AddClaim(new Claim(AbpClaimTypes.UserName, "user3"));
                identity3.AddClaim(new Claim(AbpClaimTypes.Role, "role1"));
                // identity3.AddClaim(new Claim(AbpClaimTypes.Role, "role3"));
                using (_accessor.Change(identity3))
                {

                    // var filterBuilder = GetRequiredService<IEntityTypeFilterBuilder>();
                    // var group = new DataAccessFilterGroup();
                    // group.AddRule(new DataAccessFilterRule(nameof(FakeProtectionObject.Num3), 300, DataAccessFilterOperate.LessOrEqual));

                    // var exp1 = await filterBuilder.BuildAsync(typeof(FakeProtectionObject), DataAccessOperation.Read, group);
                    // var queryable = await _repository.GetQueryableAsync();
                    // var entities = queryable.Where(exp1.Compile().As<Func<FakeProtectionObject, bool>>()).ToList();

                    var entity = await _repository.FindAsync(1);

                    var resultBuilder = GetRequiredService<IEntityPropertyResultBuilder>();
                    var exp = await resultBuilder.Build(typeof(FakeProtectionObject), DataAccessOperation.Read);
                    var comp = exp.Compile();
                    // Use non-generic interface to compile expression tree and replace the returned result
                    var resultEntity = comp.DynamicInvoke(entity).As<FakeProtectionObject>();
                    // Unauthorized fields should return default values
                    resultEntity.Protect1.ShouldBeNullOrWhiteSpace();
                }
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var identity3 = new ClaimsIdentity();
                identity3.AddClaim(new Claim(AbpClaimTypes.UserId, Guid.NewGuid().ToString()));
                identity3.AddClaim(new Claim(AbpClaimTypes.UserName, "user3"));
                identity3.AddClaim(new Claim(AbpClaimTypes.Role, "role3"));
                using (_accessor.Change(identity3))
                {
                    var entity = await _repository.FindAsync(1);

                    entity.Num3 = 300;
                    // Operations on unauthorized entities should throw an unauthorized exception
                    await Assert.ThrowsAsync<AbpAuthorizationException>(async () => await _repository.UpdateAsync(entity, true));
                }
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var entity = await _repository.FindAsync(1);
                entity.Num3 = 200;
                using (_dataFilter.Disable<IDataProtected>())
                {
                    // Data should be saved after disabling the data protection filter
                    var resultEntity = await _repository.UpdateAsync(entity);
                    resultEntity.Num3.ShouldBe(200);
                }
            });
        }
    }
}
