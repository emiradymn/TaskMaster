using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Data
{
    public class Engineer
    {
        [Key]
        [DisplayName("Mühendis No")]
        public int EngineerId { get; set; }
        [DisplayName("Mühendis Adı")]
        public string? EngineerName { get; set; }
        [DisplayName("Mühendis Soyad")]
        public string? EngineerUsername { get; set; }
        [DisplayName("Sorumlu Mühendis")]
        public string NameUsername
        {
            get
            {
                return this.EngineerName + " " + this.EngineerUsername;
            }

        }
        [DisplayName("Mail")]
        public string? EngineerEmail { get; set; }
        [DisplayName("Telefon")]
        public string? EngineerPhone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [DisplayName("İşe Başlama Tarihi")]
        public DateTime JobDateTime { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

    }
}