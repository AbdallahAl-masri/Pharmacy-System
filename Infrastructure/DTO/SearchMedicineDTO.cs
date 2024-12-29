namespace Infrastructure.DTO
{
    public class SearchMedicineDTO
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal MedicinePrice { get; set; }
        public int MedicineQTY { get; set; }
        public decimal Discount { get; set; }
    }
}
