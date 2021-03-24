using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Models.DbModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(250,MinimumLength =3,ErrorMessage ="Enter Proper Value")]
        public string CategoryName { get; set; }

    }
}
