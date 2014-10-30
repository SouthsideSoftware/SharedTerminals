using System.ComponentModel.DataAnnotations;

namespace Terminals.Data.Validation
{
    internal class DbFavoriteMetadata
    {
        // Candidate to add validation for RDP url property

        [Required]
        [StringLength(10, ErrorMessage = Validations.UNKNOWN_PROTOCOL)]
        [CustomValidation(typeof(CustomValidationRules), CustomValidationRules.METHOD_ISKNOWNPROTOCOL)]
        public string Protocol { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = Validations.MAX_255_CHARACTERS)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = Validations.MAX_255_CHARACTERS)]
        [CustomValidation(typeof(CustomValidationRules), CustomValidationRules.METHOD_ISVALIDSERVERNAME)]
        public string ServerName { get; set; }

        [StringLength(500, ErrorMessage = "Property maximum lenght is 500 characters.")]
        public string Notes { get; set; }

        [Range(0, 65535, ErrorMessage = Validations.PORT_RANGE)]
        public int Port { get; set; }
    }
}