using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Actor")]
    public class Actor
    {
        [Column(name: "actor_id")]
        public short Id { get; set; }
        [Required]
        [MaxLength(45)]
        [Column(name: "first_name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(45)]
        [Column(name: "last_name")]
        public string LastName { get; set; }
        [Required]
        [Column(name: "last_update")]
        public DateTime LastUpdate { get; set; }

        public ICollection<Film>? Films { get; set; }
        public override string ToString()
        {
            string fullName = FirstName + LastName == ""
                ? "Unknown Actor"
                : $"{FirstName} {LastName}";
            string formattedName = fullName.PadRight(20);

            return $"Actor ID: {Id,-5} | Name: {formattedName} | Last Update: {LastUpdate:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
