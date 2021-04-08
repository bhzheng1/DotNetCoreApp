using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace One.UI.APICaller
{
    [Produces("application/json")]
    [Route("api/FileDownloadService")]
    public class FileDownloadServiceController:Controller
    {
        public Task<IActionResult> DownloadDocument()
        {
            var memoryStream = new MemoryStream();
            return null;
        }
    }
}
