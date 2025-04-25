namespace backend.DTO.AnalyticsDTO
{
    public class StudentAnalyticsDTO
    {
        public ICollection<MarkAnalyticsDTO> Marks { get; set; }
        public ICollection<string> TimeLabels { get; set; }
    }
}
