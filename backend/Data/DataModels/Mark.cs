namespace backend.Data.DataModels
{
    public class Mark
    {
        public int ReportId { get; set; }
        public int MarkTypeId { get; set; }
        public int Score { get; set; }

        public Report Report { get; set; }
        public MarkType MarkType { get; set; }
    }
}
