using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class MarkType
    {
        [BindNever]
        public int TypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Mark> Marks { get; set; }
    }
}
