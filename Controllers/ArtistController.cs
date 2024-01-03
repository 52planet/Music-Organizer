using F2021A5LB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5LB.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        // GET: Artist
        private Manager m = new Manager();
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int id)
        {
            return View(m.ArtistGetOne(id));
        }

        // GET: Artist/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();
            form.Genre = new SelectList(m.GenreGetAll(),"Name", "Name");
            form.BirthOrStartDate = DateTime.UtcNow;
            return View(form);
        }

        // POST: Artist/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel collection)
        {
            
            if(!ModelState.IsValid)
            {
                return View(collection);
            }
                // TODO: Add insert logic here
            var addedItem = m.ArtistAddNew(collection);

            if (addedItem == null)
            {
                return View(collection);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id});
            }
                
        }


        //add album GET:
        [Authorize(Roles = "Coordinator")]
        [Route("Artist/{id}/AddAlbum")]
        public ActionResult AddAlbum(int id)
        {
            var artist_ = m.ArtistGetOne(id);

            if (artist_ == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure a form object
                var form = new AlbumAddFormViewModel();
                form.ArtistName = artist_.Name;
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                form.ReleaseDate = DateTime.UtcNow;

                //add artist as a selected element to artistslist
                var selectedArtist = m.ArtistGetAll().Select(a => artist_.Id);
                //config multiselect for artists
                form.ArtistsList = new MultiSelectList
                    (m.ArtistGetAll(),
                     dataValueField: "Id",
                     dataTextField: "Name",
                     selectedValues: selectedArtist);
                //config multi select for Tracks
                form.TrackList = new MultiSelectList
                    (m.TrackGetAllByArtistId(artist_.Id),
                     dataValueField: "Id",
                     dataTextField: "Name");

                return View(form);
            }
        }
        [Authorize(Roles = "Coordinator")]
        [Route("Artist/{id}/AddAlbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            // TODO: Add insert logic here
            var addedItem = m.AlbumAddNew(collection);

            if (addedItem == null)
            {
                return View(collection);
            }
            else
            {
                
                return RedirectToAction("Details", "Album", new { id = addedItem.Id });
            }
        }


        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
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

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
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
