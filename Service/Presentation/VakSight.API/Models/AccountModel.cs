using System.ComponentModel.DataAnnotations;
using VakSight.Shared;

namespace VakSight.API.Models
{
    public abstract class AccountModel
    {
        [Required(AllowEmptyStrings = true)]
        [EmailAddress]
        [RegularExpression(CommonConsts.EmailRegex, ErrorMessage = ErrorMessageConsts.InvalidEmailAddress)]
        [MaxLength(FieldLengthConsts.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(FieldLengthConsts.MaxPasswordLength)]
        [MinLength(FieldLengthConsts.MinPasswordLength)]
        public string Password { get; set; }
    }
}
