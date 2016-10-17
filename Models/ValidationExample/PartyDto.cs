using MVCLearning.CustomDataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVCLearning.Models.ValidationExample
{
    public class PartyDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        public virtual string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is mandatory")]
        public virtual string FirstName { get; set; }

        // [NationalNumberRequiredWhen(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "NationalNumberRequired")]
        // [NationalNumber("BirthDate", "GenderId", "PartyDetail.CurrentAddress.CountryId", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "NationalNumberInvalid")]
        public virtual string NationalNumber { get; set; }

        // [CardNumber("NationalityId", "CurrentAddress.CountryId", new object[] { Nationality_Belgium, Country_Belgium }, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "IDCardNumberRequired")]
        public virtual string IDCardNumber { get; set; }

        //[CardDateAttribute("IDCardNumber", "CurrentAddress.CountryId", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "InvalidIDCardValidity")]
        [NotRequiredWhen("IDCardNumber", new object[] { null }, ErrorMessage = "IDCardValidity Required.")]
        public virtual DateTime? IDCardValidity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Language required")]
        public virtual String Language { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender required")]
        public virtual String Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nationality required.")]
        public virtual String Nationality { get; set; }

        //[DateRange(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "InBelgiumSinceValid")]
        //[InSinceRequired(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredField")]
        public virtual DateTime? InBelgiumSince { get; set; }

        [DataType(DataType.Date)]
        //[DateRange(-18, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "BirthDateInvalid")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "BirthDateRequired")]
        public virtual DateTime? BirthDate { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "BirthPlaceRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public virtual string BirthPlace { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "CivilStateRequired")]
        //[RequiredWhenNotEqual("IsPartnerAvailable", "PartnerCivilStateId", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "CivilStateEqualToPartner")]
        public virtual String CivilState { get; set; }

        public virtual String MarriageContract { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "NumberOfDependentChildrenRequired")]
        public virtual int NumberOfDependentChildren { get; set; }

        //[RegularExpression("0.*", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PhoneNumberInValid")]
        //[PhoneNumberValidator("CurrentAddress.CountryId", "LoanRequestRole.MainBorrower", "ExpectedPhoneNumbers", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "IncorrectPhoneNumber")]
        //[PhoneNumberValidator("CurrentAddress.CountryId", "ExpectedPhoneNumbers", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "IncorrectPhoneNumber")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PhoneNumberRequired")]
        //[RequiredWhen("LoanRequestRole.MainBorrower", new object[] { true }, ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public virtual string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+@.+[.].+", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EmailInvalid")]
        public virtual string Email { get; set; }

        //  [RequiredWhen("BIC", new object[] { null }, true, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PleaseaddIBAN")]
        //  [BelgiumIBAN(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "IncorrectIBAN")]
        public virtual string IBAN { get; set; }

        // [RequiredWhen("IBAN", new object[] { null }, true, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PleaseaddBIC")]
        //[ValidBIC(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "IncorrectBIC")]
        public virtual string BIC { get; set; }

        public virtual String ProfessionCategory { get; set; }

        public virtual String Profession { get; set; }
    }
}