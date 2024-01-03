using F2021A5LB.EntityModels;
using F2021A5LB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5LB.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        // GET: Track
        private Manager m = new Manager();
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int id)
        {
            return View(m.TrackGetOne(id));
        }

        // GET: Track/Create
        [Authorize(Roles = "Clerk")]
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
        
            var album_ = m.AlbumGetOne(id);
            var form = new TrackAddFormViewModel();
            form.Genre = new SelectList(m.GenreGetAll(), "Name", "Name");
            form.AlbumId = album_.Id;
            form.AlbumName = album_.Name;
            return View(form);
        }

        // POST: Track/Create
        [Authorize(Roles = "Clerk")]
        [Route("Album/{id}/AddTrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            // TODO: Add insert logic here
            var addedItem = m.TrackAddNew(collection);

            if (addedItem == null)
            {
                return View(collection);
            }
            else
            {

                return RedirectToAction("Details",  new { id = addedItem.Id });
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
