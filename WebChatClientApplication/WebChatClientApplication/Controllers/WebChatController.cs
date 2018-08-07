using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChatClientApplication.Models;

namespace WebChatClientApplication.Controllers
{
    public class WebChatController : Controller
    {
        // GET: WebChat
        public ActionResult Index()
        {
            var vm = new WebChatModel();
            return View(vm);
        }
    }
}