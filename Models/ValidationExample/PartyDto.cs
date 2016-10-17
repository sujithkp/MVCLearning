using MVCLearning.CustomDataAnnotations;
using MVCLearning.Translations.ErrorMessages;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCLearning.Models.ValidationExample
{
    public class PartyDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        public virtual string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        public virtual string FirstName { get; set; }

        public virtual string NationalNumber { get; set; }

        public virtual string IDCardNumber { get; set; }

        [NotRequiredWhen("IDCardNumber", new object[] { null }, ErrorMessage = "IDCardValidity Required.")]
        public virtual DateTime? IDCardValidity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Language required")]
        public virtual String Language { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender required")]
        public virtual String Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nationality required.")]
        public virtual String Nationality { get; set; }

        [RequiredWhen("Nationality", new object[] { "Belgium" }, ErrorMessage = "In Belgium Since required.")]
        public virtual DateTime? InBelgiumSince { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? BirthDate { get; set; }

        public virtual string BirthPlace { get; set; }

        public virtual String CivilState { get; set; }

        public virtual String MarriageContract { get; set; }

        public virtual int NumberOfDependentChildren { get; set; }

        public virtual string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+@.+[.].+", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EmailInvalid")]
        public virtual string Email { get; set; }

        public virtual string IBAN { get; set; }

        public virtual string BIC { get; set; }

        public virtual String ProfessionCategory { get; set; }

        public virtual String Profession { get; set; }
    }
}