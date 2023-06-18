using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneNumbers;

namespace Wedding.Data.Mappings
{
    public class PhoneNumberConverter : ValueConverter<PhoneNumber, string>
    {
        public PhoneNumberConverter(ConverterMappingHints mappingHints = null)
            : base(
                  phoneNumber => phoneNumber == null ? string.Empty : phoneNumber.ToString(),
                  phoneNumberString => string.IsNullOrEmpty(phoneNumberString) ? null : PhoneNumberUtil.GetInstance().Parse(phoneNumberString, "US"))
        {
        }
    }
}
