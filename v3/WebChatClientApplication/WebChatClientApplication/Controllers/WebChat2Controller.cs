using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebChatClientApplication.Models;

namespace WebChatClientApplication.Controllers
{
    public class WebChat2Controller : Controller
    {
        // GET: WebChat2
        public ActionResult Index()
        {
            var vm = new WebChat2Model();
            return View(vm);
        }
    }
}