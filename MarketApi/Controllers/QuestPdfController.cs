using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestPdfController : ControllerBase
    {
        [HttpGet]
        [Route("sample1")]
        public async Task<ActionResult> GenerateSample1Pdf()
        {
            var pdfReportInfo = await QuestPdfService.GenerateSample1Pdf();
            return File(pdfReportInfo.ByteArray, pdfReportInfo.MimeType, pdfReportInfo.FileName);
        }

    }
}
