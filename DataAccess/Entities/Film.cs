using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    [Table("film")]
    public class Film
    {
        [Column(name: "film_id")]
        public short Id { get; set; }

        [Required]
        [Column(name: "title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column(name: "description")]
        public string Description { get; set; }

        [Column(name: "release_year")]
        public short ReleaseYear { get; set; }

        [Column(name: "language_id")]
        public int LanguageId { get; set; }

        [Column(name: "original_language_id")]
        public int? OriginalLanguageId { get; set; }

        [Column(name: "rental_duration")]
        public byte RentalDuration { get; set; }

        [Column(name: "rental_rate")]
        public decimal RentalRate { get; set; }

        [Column(name: "length")]
        public short Length { get; set; }

        [Column(name: "replacement_cost")]
        public decimal ReplacementCost { get; set; }

        [Column(name: "last_update")]
        public DateTime LastUpdate { get; set; }
        public ICollection<Actor>? Actors { get; set; }

        public override string ToString()
        {
            string filmSummary = string.IsNullOrEmpty(Description) ? "..." : Description.Substring(0, 35);

            return $"Film ID: {Id,-5} | Title: {Title,-20} | Release Year: {ReleaseYear,-4} | Description: {filmSummary}";
        }
    }
}
