using IronPdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Second.Model;
using Second.WebUI.Utils;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Second.WebUI.Controllers
{
    public class PdfController : Controller
    {
        private IViewRenderService _viewService;
        private IWebHostEnvironment _env;
        public PdfController(IViewRenderService viewService, IWebHostEnvironment env)
        {
            _viewService = viewService;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = new PdfModel { Header = "hello world", Body = "what is up?" };
            return View(model);
        }

        public async Task<FileResult> ExportPdf(string strModel)
        {
            var view = "Pdf/PdfFile";
            var model = JsonSerializer.Deserialize<PdfModel>(strModel);
            var html = await _viewService.RenderToStringAsync(view, model);

            var cssFile = Path.Combine(_env.WebRootPath, "css");
            cssFile = Path.Combine(cssFile, "site.css");
            var sitecss = System.IO.File.ReadAllText(cssFile);

            var re = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            html = re.Replace(html, "");
            var sb = new StringBuilder("");
            sb.Append("<style>");
            sb.Append(sitecss);
            sb.Append("</style>");
            sb.Append(html);

            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.Letter;
            var pdfDocument = htmlToPdf.RenderHtmlAsPdf(sb.ToString());
            return File(pdfDocument.BinaryData, "application/pdf", "aa.pdf");
        }
    }
}
