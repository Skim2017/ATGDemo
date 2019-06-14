using ATG.DAL;
using ATG.DAL.DatabaseModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATG.Angular.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IATGRepository atgRepo;

        public HomeController(IATGRepository atgRepo)
        {
            this.atgRepo = atgRepo;
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Index()
        {
            string myUserAgent = Request.Headers[HeaderNames.UserAgent];

            var userAgent0 = myUserAgent.Replace("Mozilla/5.0 ", "");
            var userAgent1 = userAgent0.Replace(" Safari/537.36", "");
            var userAgent2 = userAgent1.Replace(" AppleWebKit/537.36 (KHTML, like Gecko) ", "|");
            var userAgent = userAgent2.Split('|');

            System.Net.IPAddress requstIP = Request.HttpContext.Connection.RemoteIpAddress;
            ATGVisitor visitor = new ATGVisitor
            {
                IPAddress = requstIP.ToString(),
                OS = userAgent[0],
                Browser = userAgent[1],
            };

            List<ATGVisitor> visitorList = await this.atgRepo.GetAllVisitors();
            int countM = visitorList.Where(_ => _.SexID == 1).Count();
            int countF = visitorList.Where(_ => _.SexID == 2).Count();
            visitor.malePercent = countM + countF == 0 ? 0 : (int)decimal.Round((decimal)countM * 100 / (countM + countF));
            visitor.femalePercent = countM + countF == 0 ? 0 : 100 - visitor.malePercent;
            visitor.VisitorID = await this.atgRepo.CreateNewVisitor(visitor);

            return Ok(visitor);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Index([FromBody] ATGVisitor visitor)
        {
            var isUpdated = await atgRepo.UpdateByID(visitor);
            
            return Json(visitor);
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicy")]
        public IActionResult Privacy()
        {
            string myUserAgent = Request.Headers[HeaderNames.UserAgent];

            var test0 = myUserAgent.Replace("Mozilla/5.0 ", "");
            var test1 = test0.Replace(" Safari/537.36", "");
            var test2 = test1.Replace(" AppleWebKit/537.36 (KHTML, like Gecko) ", "|");
            var test3 = test2.Split('|');

            var os = test3[0];
            var broswer = test3[1];

            System.Net.IPAddress requstIP = Request.HttpContext.Connection.RemoteIpAddress;
            ViewBag.IP = requstIP;
            ViewBag.OS = os;
            ViewBag.BROWSER = broswer;
            return View();
        }
    }
}
