using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTO
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string MedicineName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int MedicineDepartmentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Description { get; set; }

        //public IFormFile MedicineImage { get; set; }
        public string ImageName { get; set; }
        public string ImageFullPath { get; set; }
        public string ImageReadPath { get; set; }

        //public List<MedicineDepartmentDTO> MedicineDepartmentsList { get; set; }

        public string DepartmentName { get; set; }
    }
}
