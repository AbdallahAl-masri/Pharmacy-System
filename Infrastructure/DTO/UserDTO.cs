using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string FirstName { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string LastName { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly DateOfBirth { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Mobilenumber { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public bool Gender { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Address { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public bool ShiftType { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int Salary { get; set; }

        public int JobDescriptionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly JoinDate { get; set; }

        //public DateOnly? ResignationDate { get; set; }

        public bool IsActive { get; set; }

        public string GenderDisplayName { get; set; }
        public string ShiftTypeName { get; set; }
        public string JobDescriptionName { get; set; }

        public List<JobDescriptionDTO> JobDescriptionsList { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int DepartmentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int SectionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}
