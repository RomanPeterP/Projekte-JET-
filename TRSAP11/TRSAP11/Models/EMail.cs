using System.Text.RegularExpressions;

namespace TRSAP11.Models
{
    public readonly struct EMail
    {
        public string Value { get; }

        public EMail(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                throw new ArgumentException("Ungültige E-Mail-Adresse");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
