using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ImagesDatebase;
using PictureGallery.Models;

namespace PictureGallery.Controllers
{
    public class HomeController : Controller
    {
        private ImagesContext db =new ImagesContext();
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


        [ChildActionOnly]
        public ActionResult RenderGellery(int page=1)
        {
            ViewBag.PageAmount = db.Set<Image>().Count()%3==0?db.Set<Image>().Count()/3:db.Set<Image>().Count()/3+1;
            return Request.IsAjaxRequest() ? PartialView("_RenderGallery") : GetGalleryPage(page);
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {
            if (id == -1) return null;

            Image img = db.Images.FirstOrDefault(i => i.Id == id);

            return img == null ? null : File(img.Content, "image/jpg");
        }
        [HttpGet]
        public ActionResult GetGalleryPage(int page=1)
        {
            
                IEnumerable<Image> query = db.Set<Image>().OrderByDescending(i=>i.Id).Skip((page-1)*3).Take(3);
            int[] arr = new int[3];
            
                for (int i = 0; i < 3; i++)
                {
                    if (i >= query.Count())
                    {
                        arr[i] = -1;
                        continue;
                    }
                    arr[i] = query.ElementAt(i).Id;
                }
                if (Request.IsAjaxRequest()) return Json(arr, JsonRequestBehavior.AllowGet);

            return View("_RenderGalleryNotAjax", arr);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Image pic, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                pic.Content = imageData;

                db.Images.Add(pic);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pic);
        }
    }

}