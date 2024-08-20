using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Data
{
    public class TaskSave
    {
        [Key]
        public int SaveId { get; set; }
        public int StajyerId { get; set; }
        public Stajyer Stajyer { get; set; } = null!; // Navigation Properties
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public DateTime TaskSaveDate { get; set; }
    }
}