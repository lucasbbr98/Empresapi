using System.IO;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IHttpClientService
    {
        public Task<Stream> DownloadFile(string url);
    }
}
