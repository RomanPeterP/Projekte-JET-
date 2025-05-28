using System.Text.RegularExpressions;

namespace TRSAP11.Models
{
    public readonly struct PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^\+?\d+$"))
                throw new ArgumentException("Ungültige Telefonnummer.");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
