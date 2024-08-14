using System.Threading.Tasks;

namespace celsia.Services.Interfaces
{
    public interface IExcelImportRepository 
    {
        Task ImportFromExcelAsync(string filePath);
    }
}
