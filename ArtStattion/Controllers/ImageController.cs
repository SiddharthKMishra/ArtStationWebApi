using ArtStationWebApi.Models;
using MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace ArtStattion.Controllers
{

    public class ImageController : Controller
    {
        // GET: Image
        [HttpGet]
        [Authorize]
        public ActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult AddImage(Image imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  //datetime is added only to avoid duplicate file name situations
            imageModel.ImagePath = "~/Image/" + fileName;        //here we are saving the relative path ofthe image into our model
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);      //exact path
            imageModel.ImageFile.SaveAs(fileName);          //saving the specific image to the folder "Image"

            //now we want to save the relative file location to the image table, saving exact path may cause privacy issues

            using (DBModel db = new DBModel())
            {
                db.Images.Add(imageModel);
                db.SaveChanges();
            }

            ModelState.Clear();
            return View();
        }


        public ActionResult DeleteImage(int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Images/" + id.ToString()).Result;    //calling DeleteCommisionRequest to delete specific id data
            TempData["SuccessMessage"] = "Removed image Successfully";

            return RedirectToAction("Gallery");
        }

        [HttpGet]
        public ActionResult Gallery()
        {
            DBModel db = new DBModel();
            //List<Image> AllImages = db.Images.ToList();
            return View(db.Images);
        }

       
    }
}