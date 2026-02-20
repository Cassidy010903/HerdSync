using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base
{
    public abstract class BaseHistoryModel
    {
        [Key]
        public Guid HistoryId { get; set; }

        public Guid OriginalId { get; set; }
        public DateTime DeletedDateTime { get; set; }
        public string DeletedUser { get; set; } = "System";  //Update this to take the name of the logged-in user
    }
}