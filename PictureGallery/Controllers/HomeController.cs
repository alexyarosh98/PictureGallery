using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImagesDatebase;
using PictureGallery.Models;

namespace PictureGallery.Controllers
{
    public class HomeController : Controller
    {
        private ImagesContext db =new ImagesContext();
        private DataContext dc=new DataContext(@"data source=(localdb)\MSSQLLocalDB;initial catalog=ImagesDB;integrated security=True");
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
        public ActionResult RenderGellery()
        {
            return PartialView("_RenderGallery");
        }

        [HttpGet]
        public ActionResult GetImage(int id)
        {

            //PictureModel picture=new PictureModel();
            //picture.FilePath = "/Images/Image3_big.jpg";
            //picture.Description = "world";
            //picture.Content = db.Images.FirstOrDefault().Content;

            Image img = db.Images.FirstOrDefault(i => i.Id == id);

            return img == null ? null : File(img.Content, "image/jpg");

          // return Json(picture.Content, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GetGalleryPage(int page=1)
        {
            int count = dc.GetTable<Image>().Count();
            if (page != 1)
            {
                
                int[] arr=new int[3];
                for (int i = 0; i < 3; i++)
                {
                }
                //var dsadsad = imgInfos.Take(3).Select(i=>i.Id);
                //var img = imgInfos.Skip(page * 3).Take(3);
                //db.Images.OrderBy(i => i.Id)
                //    .Skip(page * 3)
                //    .Take(3)
                //    .ToList()
                //    .Select(i => new {Id = i.Id});
                // Debug.WriteLine("&&&&&&&&&&&&&&&&&&"+img.Count());
                
                int[] ar = new int[]{2, 3, 4};
                return Json(ar, JsonRequestBehavior.AllowGet);
            }
            int[] atr = new int[] {3, 4, 5};
            return Json(atr,JsonRequestBehavior.AllowGet);

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
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                pic.Content = imageData;

                db.Images.Add(pic);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pic);
        }
    }

}