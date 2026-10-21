# LINGYUN.Abp.Data.DbMigrator

Data migration module, used to cooperate with scenarios that require data initialization (such as when a new tenant is created)

## Configuration usage


```csharp
[DependsOn(typeof(AbpDataDbMigratorModule))]
public class YouProjectModule : AbpModule
{
  // other
}

public class FakeDbContext : AbpDbContext<FakeDbContext>
{
	public FakeDbContext(DbContextOptions<FakeDbContext> options)
		: base(options)
    {
        
     }

	 protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
		base.OnModelCreating(modelBuilder);
     }
}

public class FakeMigretor 
{
	private readonly IDbSchemaMigrator _migrator;

	public FakeMigretor(
		IDbSchemaMigrator migrator)
	{
		_migrator = migrator;
	}

	public async Task MigrateAsync()
	{
		// The EF migration script will be executed, and the DbContext used for data migration needs to be provided
		await _migrator.MigrateAsync<FakeDbContext>(
			// connectionString: current tenant connection string, used to build connections
			// builder: Constructor used to initialize the context
			(connectionString, builder) =>
			{

				// The user determines the database parameters in the delegate
				builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

				// Return the instantiated DbContext
				return new FakeDbContext(builder.Options);
            });
	}
}
```
