using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    /// <summary>
    /// Clase de acceso a datos.
    /// Nota: No haremos referencias a la clase User. Esta clase debe ser agnostica al tipos dle dominio
    /// </summary>
    public class DataAccess
    {
        public async static Task<string[]> ReadUsersFromFile(string filename)
        {            
            var filenameFullPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Users.txt");

            return await File.ReadAllLinesAsync(filenameFullPath);
        }
    }
}

