namespace backend.DTO.AnalyticsDTO
{
    public class MarkAnalyticsDTO
    {
        public string MarkLabel { get; set; }
        public ICollection<double> Data { get; set; }
    }
}
