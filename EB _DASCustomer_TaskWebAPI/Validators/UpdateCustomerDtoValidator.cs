using EB__DASCustomer_TaskWebAPI.Dtos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EB__DASCustomer_TaskWebAPI.Validators
{
    public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("E Mail Formatında mail adresi giriniz lütfen")
                .NotEmpty().WithMessage("Boş vey null olamaz");
            RuleFor(x => x.PhoneNumber)
             .MaximumLength(10).WithMessage("Telefon numarası en fazla 10 karakter olabilir!")
              .MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olabilir!")
             .Must(IsPhoneValid).WithMessage("Lütfen rakam giriniz veya 5 ile başlamalısınız!");
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("İsim alanı boş geçilemez lütfen isminizi giriniz!");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soy İsim alanı boş geçilemez lütfen isminizi giriniz!");
            RuleFor(x => x.BirthDay)
                .NotEmpty().WithMessage("Dogum Tarihi alanı boş geçilemez lütfen isminizi giriniz!")
               .LessThanOrEqualTo(DateTime.Now).WithMessage("Doğum tarihi bugünün tarihini geçmemelidir!");

        }
        public static bool IsPhoneValid(string mobilePhone)
        {
            if (string.IsNullOrWhiteSpace(mobilePhone))
                return false;

            mobilePhone = Regex.Replace(mobilePhone, "[^0-9]", "");
            return mobilePhone.StartsWith("5");
        }
    }
}
