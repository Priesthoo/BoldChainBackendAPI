using System.Text.RegularExpressions;

namespace BoldChainBackendAPI.BoldChainException
{
    public static class ValidatiionHelper
    {
        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password cannot be empty." } }
                });
            }
            if (password.Length < 8)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password must be at least 8 characters long." } }
                });
            }
            if (!password.Any(char.IsUpper))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password must contain at least one uppercase letter." } }
                });
            }
            if (!password.Any(char.IsLower))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password must contain at least one lowercase letter." } }
                });
            }
            if (!password.Any(char.IsDigit))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password must contain at least one digit." } }
                });
            }
            if (!password.Any(ch => "!@#$%^&*()_+[]{}|;:,.<>?".Contains(ch)))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Password", new[] { "Password must contain at least one special character." } }
                });
            }
        }
        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "Email cannot be empty." } }
                });
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        { "Email", new[] { "Invalid email format." } }
                    });
                }
            }
            catch
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Email", new[] { "Invalid email format." } }
                });
            }
        }
        public static void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Username", new[] { "Username cannot be empty." } }
                });
            }
            if (username.Length < 3 || username.Length > 20)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Username", new[] { "Username must be between 3 and 20 characters long." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Username", new[] { "Username can only contain letters, numbers, and underscores." } }
                });
            }
        }
        public static void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "PhoneNumber", new[] { "Phone number cannot be empty." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{1,14}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "PhoneNumber", new[] { "Invalid phone number format." } }
                });
            }
        }
        public static void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "DateOfBirth", new[] { "Date of birth cannot be in the future." } }
                });
            }
            if (dateOfBirth < DateTime.Now.AddYears(-120))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "DateOfBirth", new[] { "Date of birth is too far in the past." } }
                });
            }
        }
        public static void ValidateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Url", new[] { "URL cannot be empty." } }
                });
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Url", new[] { "Invalid URL format." } }
                });
            }
        }
        public static void ValidateCreditCardNumber(string creditCardNumber)
        {
            if (string.IsNullOrWhiteSpace(creditCardNumber))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardNumber", new[] { "Credit card number cannot be empty." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(creditCardNumber, @"^\d{16}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardNumber", new[] { "Credit card number must be 16 digits long." } }
                });
            }
        }
        public static void ValidatePostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "PostalCode", new[] { "Postal code cannot be empty." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(postalCode, @"^\d{5}(-\d{4})?$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "PostalCode", new[] { "Invalid postal code format." } }
                });
            }
        }
        public static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "Name cannot be empty." } }
                });
            }
            if (name.Length < 2 || name.Length > 50)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "Name must be between 2 and 50 characters long." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Name", new[] { "Name can only contain letters and spaces." } }
                });
            }
        }
        public static void ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Address", new[] { "Address cannot be empty." } }
                });
            }
            if (address.Length < 5 || address.Length > 100)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Address", new[] { "Address must be between 5 and 100 characters long." } }
                });
            }


        }
        public static void ValidateCreditCardExpiryDate(DateTime expiryDate)
        {
            if (expiryDate < DateTime.Now)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardExpiryDate", new[] { "Credit card expiry date cannot be in the past." } }
                });
            }
            if (expiryDate > DateTime.Now.AddYears(10))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardExpiryDate", new[] { "Credit card expiry date cannot be more than 10 years in the future." } }
                });
            }
        }
        public static void ValidateCreditCardCVV(string cvv)
        {
            if (string.IsNullOrWhiteSpace(cvv))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardCVV", new[] { "Credit card CVV cannot be empty." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(cvv, @"^\d{3,4}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardCVV", new[] { "Credit card CVV must be 3 or 4 digits long." } }
                });
            }
        }
        public static void ValidateSocialSecurityNumber(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "SocialSecurityNumber", new[] { "Social Security Number cannot be empty." } }
                });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(ssn, @"^\d{3}-\d{2}-\d{4}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "SocialSecurityNumber", new[] { "Invalid Social Security Number format. Use XXX-XX-XXXX." } }
                });
            }
        }
        public static void ValidateIPAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "IPAddress", new[] { "IP Address cannot be empty." } }
                });
            }
            if (!System.Net.IPAddress.TryParse(ipAddress, out _))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "IPAddress", new[] { "Invalid IP Address format." } }
                });
            }
        }
        public static void ValidateBlockChainAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "BlockChainAddress", new[] { "Blockchain address cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(address, @"^0x[a-fA-F0-9]{40}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "BlockChainAddress", new[] { "Invalid Blockchain address format. It should start with '0x' followed by 40 hexadecimal characters." } }
                });
            }

        }
        public static void ValidateHexadecimalString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "HexadecimalString", new[] { "Hexadecimal string cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(hexString, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "HexadecimalString", new[] { "Invalid hexadecimal string format." } }
                });
            }
        }
        public static void ValidateBase64String(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Base64String", new[] { "Base64 string cannot be empty." } }
                });
            }
            try
            {
                Convert.FromBase64String(base64String);
            }
            catch (FormatException)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Base64String", new[] { "Invalid Base64 string format." } }
                });
            }
        }
        public static void ValidateUUID(string uuid)
        {
            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "UUID", new[] { "UUID cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(uuid, @"^[{(]?[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}[)}]?$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "UUID", new[] { "Invalid UUID format." } }
                });
            }
        }
        public static void ValidateColorHexCode(string colorHexCode)
        {
            if (string.IsNullOrWhiteSpace(colorHexCode))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "ColorHexCode", new[] { "Color hex code cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(colorHexCode, @"^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "ColorHexCode", new[] { "Invalid color hex code format." } }
                });
            }
        }
        public static void ValidateLatitude(double latitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Latitude", new[] { "Latitude must be between -90 and 90 degrees." } }
                });
            }
        }
        public static void ValidateLongitude(double longitude)
        {
            if (longitude < -180 || longitude > 180)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "Longitude", new[] { "Longitude must be between -180 and 180 degrees." } }
                });
            }
        }
        public static void ValidateTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "TimeSpan", new[] { "Time span cannot be negative." } }
                });
            }
            if (timeSpan.TotalDays > 365)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "TimeSpan", new[] { "Time span cannot exceed one year." } }
                });
            }
        }
        public static void ValidateJsonString(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "JsonString", new[] { "JSON string cannot be empty." } }
                });
            }
            try
            {
                var json = System.Text.Json.JsonDocument.Parse(jsonString);
            }
            catch (System.Text.Json.JsonException)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "JsonString", new[] { "Invalid JSON format." } }
                });
            }
        }
        public static void ValidateXmlString(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "XmlString", new[] { "XML string cannot be empty." } }
                });
            }
            try
            {
                var xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(xmlString);
            }
            catch (System.Xml.XmlException)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "XmlString", new[] { "Invalid XML format." } }
                });
            }
        }
        public static void ValidateCreditCardHolderName(string holderName)
        {
            if (string.IsNullOrWhiteSpace(holderName))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardHolderName", new[] { "Credit card holder name cannot be empty." } }
                });
            }
            if (holderName.Length < 2 || holderName.Length > 50)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardHolderName", new[] { "Credit card holder name must be between 2 and 50 characters long." } }
                });
            }
            if (!Regex.IsMatch(holderName, @"^[a-zA-Z\s]+$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CreditCardHolderName", new[] { "Credit card holder name can only contain letters and spaces." } }
                });
            }
        }
        public static void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "FilePath", new[] { "File path cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(filePath, @"^[a-zA-Z]:\\[\\\S|*\S]?.*$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "FilePath", new[] { "Invalid file path format." } }
                });
            }
        }
        public static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CurrencyCode", new[] { "Currency code cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(currencyCode, @"^[A-Z]{3}$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "CurrencyCode", new[] { "Invalid currency code format. It should be a 3-letter uppercase code." } }
                });
            }
        }
        public static void ValidateISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "ISBN", new[] { "ISBN cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(isbn, @"^(97(8|9))?\d{9}(\d|X)$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "ISBN", new[] { "Invalid ISBN format." } }
                });
            }

        }
        public static void ValidateMACAddress(string macAddress)
        {
            if (string.IsNullOrWhiteSpace(macAddress))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "MACAddress", new[] { "MAC address cannot be empty." } }
                });
            }
            if (!Regex.IsMatch(macAddress, @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$"))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "MACAddress", new[] { "Invalid MAC address format." } }
                });
            }
        }
        public static void ValidateTimeZone(string timeZone)
        {
            if (string.IsNullOrWhiteSpace(timeZone))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "TimeZone", new[] { "Time zone cannot be empty." } }
                });
            }
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "TimeZone", new[] { "Invalid time zone." } }
                });
            }
        }
        public static void ValidateMessage(string message)
        {
            if (string.IsNullOrEmpty(message) && string.IsNullOrWhiteSpace(message))
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    {"Message", new[]{ "message cannot be empty"} }

                });
            }
            if (message.Length > 3000)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    {"Message", new[]{ "message cannot be more than 3000 characters long"} }
                });
            }
        }
    }
}

