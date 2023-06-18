using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Wedding.Data.Entities
{
    public class WeddingSetup
    {
        [Required]
        [Key]
        public Guid WeddingSetupId { get; set; }

        [Required]
        /// This is the User ID in AspNet IdentityUser
        public required IdentityUser PartnerOne { get; set; }
        
        /// This is the User ID in AspNet IdentityUser
        public IdentityUser PartnerTwo { get; set; }
    }
}
