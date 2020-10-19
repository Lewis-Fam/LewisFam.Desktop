using System.IO;
using System.Text;
using System.Threading.Tasks;
using LewisFam.Desktop.Core.Contracts.Services;
using LewisFam.Common.Util;
//using LewisFam.Common.Utilities;
using Newtonsoft.Json;

namespace LewisFam.Desktop.Core.Services
{
    public class FileService : IFileService
    {


        public T Read<T>(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonUtil.DeserializeObject<T>(json);                
                //return JsonConvert.DeserializeObject<T>(json);
            }

            return default;
        }

        public void Save<T>(string folderPath, string fileName, T content)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileContent = JsonUtil.ToJson(content);
            File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
        }

        public void Delete(string folderPath, string fileName)
        {
            if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
            {
                File.Delete(Path.Combine(folderPath, fileName));
            }
        }

        public async Task<T> ReadAsync<T>(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            if (File.Exists(path))
            {
                //var json = File.ReadAllText(path);
                return await JsonUtil.DeserializeObjectAsync<T>(File.ReadAllText(path));
                //return JsonConvert.DeserializeObject<T>(json);
            }

            return default;
        }
    }
}
