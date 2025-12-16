using System.ComponentModel.DataAnnotations;

namespace ModelHandling.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50,ErrorMessage ="Name cannot be Longer than 50 characters")]

        public string Name { get; set; }

        [Range(18,60, ErrorMessage ="Age must be between 18 and 60")]

        public int Age { get; set; }

        [EmailAddress (ErrorMessage ="Invalid EmailAddress")]

        public string Email { get; set; }
    }
}
