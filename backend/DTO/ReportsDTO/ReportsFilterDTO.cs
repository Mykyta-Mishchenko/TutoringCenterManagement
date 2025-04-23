using backend.Models;

namespace backend.DTO.ReportsDTO
{
    public class ReportsFilterDTO
    {
        public bool IsSearching { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}
