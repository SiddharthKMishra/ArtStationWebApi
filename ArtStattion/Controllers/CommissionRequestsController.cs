using ArtStationWebApi.Models;
using ArtStattion.Models;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ArtStattion.Controllers
{
   
    public class CommissionRequestsController : Controller
    {
        // GET: CommissionRequest
        [Authorize(Roles ="Admin,User")]
        public ActionResult Index()
        {
            IEnumerable<CommissionRequestModel> commReqList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("CommissionRequests").Result;
            commReqList = response.Content.ReadAsAsync<IEnumerable<CommissionRequestModel>>().Result;
            return View(commReqList);
        }




        [Authorize(Roles = "Admin,User")]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new CommissionRequestModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("CommissionRequests/" + id.ToString()).Result;   //calling GetCommissionRequest to fetch specific commission request details
                return View(response.Content.ReadAsAsync<CommissionRequestModel>().Result);
            }

        }


       
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult AddOrEdit(CommissionRequestModel comReq)
        {
            if (comReq.CommID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("CommissionRequests", comReq).Result;    //calling PostCommisiionRequest to add new
                TempData["SuccessMessage"] = "Requested new Commission Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("CommissionRequests/" + comReq.CommID, comReq).Result;  //calling PutCommisiionRequest to update
                TempData["SuccessMessage"] = "Updated Commission Request Details Successfully";
            }


            return RedirectToAction("Index");
        }




        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("CommissionRequests/" + id.ToString()).Result;    //calling DeleteCommisionRequest to delete specific id data
            TempData["SuccessMessage"] = "Removed Commission Request Successfully";


            return RedirectToAction("Index");
        }
    }
}