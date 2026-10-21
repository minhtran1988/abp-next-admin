using System.Threading.Tasks;
using Volo.Abp.Data;

namespace PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.DataSeeder
{
    public interface IProjectNameDataSeeder
    {
        /// <summary>
        /// Initialize data
        /// </summary>
        /// <param name="context">Data seed context</param>
        /// <returns>Task</returns>
        Task SeedAsync(DataSeedContext context);
    }
}
