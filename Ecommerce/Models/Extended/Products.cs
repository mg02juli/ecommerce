using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Ecommerce.Models
{
    [MetadataType(typeof(ProductsMetaData))]
    public partial class Products
    {


        public string[] GetImageUrls( int number)
        {
            number = number > 3 ? 1 : 3;
            WEBprojectDBEntities DBEntities = new WEBprojectDBEntities();

            string[] url = DBEntities.Product_Image.Where(
                s => s.product_id.Equals(this.product_id))
                .Select(s => s.image_url).ToArray();
            
            return url;
        }





    }

    public class ProductsMetaData
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        [DataType(DataType.Currency)]
        public decimal product_price { get; set; }



    }


}