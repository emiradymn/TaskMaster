using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Data
{
    public class Task
    {
        [DisplayName("Görev No")]
        public int TaskId { get; set; }
        [DisplayName("Görev No")]
        public string? Title { get; set; }
        public int? EngineerId { get; set; }
        public Engineer Engineer { get; set; } = null!;   // task tablosuna mühendis sütunu ekliyoruz  (int EnggineerId = null;  )
        // public string? TaskPerson { get; set; }
        public ICollection<TaskSave> TaskSaves { get; set; } = new List<TaskSave>();
    }
}