using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICTApplication.Models
{
    public class Employee
    {
       
        public int EmpId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Name is required.")]
        [MinLength(3, ErrorMessage = "Name should be at least 3 characters.")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3,ErrorMessage = "Code should be at least 3 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Code is required.")]
        public string Code { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required( ErrorMessage = "Please select Department")]
        public int  DepId { get; set; }
        public string DepartmenName { get; set; }
        public Department Department{ get; set; }
    }
}