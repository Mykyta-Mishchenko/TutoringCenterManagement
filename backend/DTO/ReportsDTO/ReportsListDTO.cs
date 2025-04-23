namespace backend.DTO.ReportsDTO
{
    public class ReportsListDTO
    {
        public int TotalPageNumber { get; set; }
        public ICollection<ReportDTO> ReportsList { get; set; }
    }
}
