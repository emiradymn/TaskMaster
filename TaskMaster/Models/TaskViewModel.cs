using System.ComponentModel;


namespace TaskMaster.Data
{
    public class TaskViewModel
    {
        [DisplayName("Görev No")]
        public int TaskId { get; set; }
        [DisplayName("Görev No")]
        public string? Title { get; set; }
        public int? EngineerId { get; set; }

    }
}