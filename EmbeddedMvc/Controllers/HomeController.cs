using System.Security.Claims;
using System;
using EmbeddedMvc.Models;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedMvc.Controllers
{
    public class HomeController : Controller
    {  

       

        private static IList<SchowekModel> schowek = new List<SchowekModel>()

        {
            new SchowekModel(){SchwId = 1, Name = "Schowek test 1", Description ="XP41123SDCMDASDDA", Done = false}
        };

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]

    
        public ActionResult Schoweklista()
        {
            return View(schowek);
        }

        [Authorize]

        public ActionResult Details(int id)
        {
            return View(schowek.FirstOrDefault(x => x.SchwId == id));
        }
        

        public ActionResult Edit(int id)
        {
            return View(schowek.FirstOrDefault(x => x.SchwId == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, SchowekModel schowekModel)
        {
            SchowekModel schowki = schowek.FirstOrDefault(x => x.SchwId == id);
            schowki.Name = schowekModel.Name;
            schowki.Description = schowekModel.Description;
            schowki.Done = schowekModel.Done;

            return RedirectToAction(nameof(Schoweklista));

        }
        public ActionResult Delete(int id)
        {
            return View(schowek.FirstOrDefault(x => x.SchwId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Delete(int id, SchowekModel schowekModel)
        {
            SchowekModel schowki = schowek.FirstOrDefault(x => x.SchwId == id);
            schowek.Remove(schowki);
            return RedirectToAction(nameof(Schoweklista));
        }
        [Authorize]
        public ActionResult About()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        [ResourceAuthorize("Read", "ContactDetails")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new SchowekModel());
        }

        [HandleForbidden]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchowekModel schowekModel)
        
            
            {
                schowekModel.SchwId = schowek.Count + 1;
                schowek.Add(schowekModel);

                return RedirectToAction(nameof(Schoweklista));
            }
            
        


     

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}