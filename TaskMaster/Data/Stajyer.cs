using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Data
{
    public class Stajyer
    {
        // id => primary key
        //eğer verilen namede Id gibi anlaşılabilir bişi yoksa (Mesela Kimlik No gibi) bu primary key yapmak için kullanılır.

        [Key]
        [DisplayName("ID")]
        public int StajyerId { get; set; }
        [DisplayName("Stajyer Adı")]
        public string? StajyerName { get; set; }
        [DisplayName("Stajyer Soyadı")]
        public string? StajyerUsername { get; set; }

        public string NameUsername
        {

            get
            {
                return this.StajyerName + " " + this.StajyerUsername;
            }

        }
        [DisplayName("Stajyer Mail")]
        public string? Email { get; set; }
        [DisplayName("Stajyer Telefon Numarası")]
        public string? Phone { get; set; }
        [DisplayName("ID")]
        public DateTime StajDate { get; set; }
        public ICollection<TaskSave> TaskSaves { get; set; } = new List<TaskSave>(); // tasksaves modelinin içinde yabancı primary keyler var bu keyleri toptan getiriyor. new() null olmasına karşı kullnaılıyor.
    }
}