using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ModernHome.Utility
{
    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string expiryDate = value.ToString();
                DateTime dateTime;
                bool isValidFormat = DateTime.TryParseExact(expiryDate, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                if (isValidFormat && dateTime > DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Neispravan datum isteka kartice!");
        }
    }
}
