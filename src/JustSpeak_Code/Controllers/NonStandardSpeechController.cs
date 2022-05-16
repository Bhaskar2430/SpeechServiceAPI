using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustSpeak.Controllers
{
    public class NonStandardSpeechController : Controller
    {
        // GET: NonStandardSpeech
        public ActionResult Index()
        {
            ViewData["selectedNonStandard"] = "active";
            return View();
        }

        // GET: NonStandardSpeech/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NonStandardSpeech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NonStandardSpeech/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NonStandardSpeech/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NonStandardSpeech/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NonStandardSpeech/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NonStandardSpeech/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
