using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Wedding.Data;
using Wedding.Services;

namespace Wedding.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GuestListController : Controller
    {
        private readonly IPartyService _partyService;
        public GuestListController(IServiceProvider serviceProvider) {
            _partyService = serviceProvider.GetService<IPartyService>();
        }
        public IActionResult Index()

        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGuestList()
        {

            var parties = await _partyService.GetAllAsync(true);

            parties.ToExcelFile("./temp.xlsx", true);
            var memoryStream = new FileStream("./temp.xlsx", FileMode.Open);
            memoryStream.Position = 0;
            // Return the file as a stream result
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "guests.xlsx",
            };
        }
    }
}
