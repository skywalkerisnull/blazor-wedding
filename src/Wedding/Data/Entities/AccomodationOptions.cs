using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class AccomodationOptions
    {
        [Required]
        [Key]
        public Guid AccomodationOptionsId { get; set; }

        [Required]
        public WeddingSetup WeddingSetup { get; set; }

        [Required]
        public string AccomodationName { get; set; }
        [Required]
        public string AccomodationType { get; set; }
        [Required]
        [StringLength(500)]
        public string AccomodationDescription { get; set; }
        [Required]
        public Uri AccomodationUrl { get; set;}
        public PhoneNumber PhoneNumber { get;set;}

        [Required]
        public Picture Picture { get; set;}
    }
}
