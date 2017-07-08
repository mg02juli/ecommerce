using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Ecommerce.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        public string cnf_password { get; set; }

    }

    public class CustomerMetaData {

            [Required(ErrorMessage = "Please enter email address", AllowEmptyStrings = false)]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            [DataType(DataType.EmailAddress)]
        public string emailID { get; set; }
            [Required(ErrorMessage = "Please enter first name", AllowEmptyStrings = false)]
        public string first_name { get; set; }
            [Required(ErrorMessage = "Please enter last name", AllowEmptyStrings = false)]
        public string last_name { get; set; }
            [Required(ErrorMessage = "Please enter password", AllowEmptyStrings = false)]
            [DataType(DataType.Password)]
            [StringLength(50, ErrorMessage = "Password lengh should be atleast 8 character", MinimumLength = 8)]
        public string password { get; set; }
            [Required(ErrorMessage = "Please confirm password", AllowEmptyStrings = false)]
            [Compare("password", ErrorMessage = "Password doesn't match")]
        public string cnf_password { get; set; }
            [Required(ErrorMessage = "Please enter phone number", AllowEmptyStrings = false)]
            [StringLength(20, ErrorMessage = "minimum 8 digit", MinimumLength = 8)]
        public string phone_number { get; set; }
            [Required(ErrorMessage = "Please enter home address", AllowEmptyStrings = false)]
        public string address_line_1 { get; set; }

    }
}