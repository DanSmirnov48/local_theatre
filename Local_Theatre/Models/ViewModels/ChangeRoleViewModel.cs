using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Local_Theatre.Models.ViewModels
{
    //ChangeRoleViewModel class to update from an old to a new Role
    public class ChangeRoleViewModel
    {
        public string UserName { get; set; }
        public string OldRole { get; set; }

        public ICollection<SelectListItem> Roles { get; set; }
        [Required]
        public string Role { get; set; }
    }
}