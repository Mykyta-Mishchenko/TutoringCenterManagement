namespace backend.DTO.AnalyticsDTO
{
    public class AnalyticsFilterDTO
    {
        public bool IsSearching { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int Months { get; set; }
    }
}
