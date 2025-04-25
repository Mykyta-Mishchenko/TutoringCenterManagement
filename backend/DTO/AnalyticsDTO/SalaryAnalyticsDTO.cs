namespace backend.DTO.AnalyticsDTO
{
    public class SalaryAnalyticsDTO
    {
        public ICollection<int> Data { get; set; }
        public ICollection<string> TimeLabels { get; set; }
    }
}
