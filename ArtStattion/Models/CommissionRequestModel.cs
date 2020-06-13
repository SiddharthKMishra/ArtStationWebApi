using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtStattion.Models
{
    public class CommissionRequestModel
    {
        public int CommID { get; set; }



        [Required(ErrorMessage ="This Field is Required")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }



        [Display(Name = "Commission Description")]
        public string CommDesc { get; set; }

        
        [Display(Name = "Type Of Commission")]
        public string CommTypeID { get; set; }


        [Display(Name = "Commission Status")]
        public string Status { get; set; }


        [Display(Name ="Approval Status")]
        public string Approval { get; set; }
        public string Title
        {
            get
            {
                return CommID != 0 ? "Edit Commission Request Details" : "New Commission Request";
            }
        }
    }
}
