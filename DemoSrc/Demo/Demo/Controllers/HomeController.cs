using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Service;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly Lazy<ITestService> _testService;

        public HomeController(Lazy<ITestService> testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {



            return View();
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetAllAdmins()
        {
            var admins = this._testService.Value.GetAllAdmin();

            return View(admins);
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin model)
        {
            var typeid = this._testService.Value.CheckAdminType();
            model.Typeid = typeid;
            var result = this._testService.Value.Add(model);

            if (result)
            {
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }

            //return View();

        }


        public async Task<ActionResult> GetAllAdminsASync()
        {
            var admins = await this._testService.Value.GetAllAdminASync();

            return View(admins);
        }

        public ActionResult AddAdminASync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ContentResult> AddAdminASync(Admin model)
        {
            var typeid = await this._testService.Value.CheckAdminTypeASync();
            model.Typeid = typeid;
            var result = await this._testService.Value.AddAsync(model);

            if (result)
            {
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
            //return View();
        }
    }
}