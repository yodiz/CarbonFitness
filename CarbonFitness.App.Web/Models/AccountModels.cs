using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;

namespace CarbonFitness.App.Web.Models {
	public interface IFormsAuthenticationService {
		void SignIn(string userName, bool createPersistentCookie);
		void SignOut();
	}

	public class FormsAuthenticationService : IFormsAuthenticationService {
		public void SignIn(string userName, bool createPersistentCookie) {
			ValidationUtil.ValidateRequiredStringValue(userName, "userName");

			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}

		public void SignOut() {
			FormsAuthentication.SignOut();
		}
	}

	internal static class ValidationUtil {
		private const string stringRequiredErrorMessage = "Value cannot be null or empty.";

		public static void ValidateRequiredStringValue(string value, string parameterName) {
			if (String.IsNullOrEmpty(value)) {
				throw new ArgumentException(stringRequiredErrorMessage, parameterName);
			}
		}
	}

    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "Det nya lösenordet och det bekräftade lösenorden måste vara lika.")]
	public class ChangePasswordModel {
		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Nuvarande lösenord")]
		public string OldPassword { get; set; }

		[Required]
		[ValidatePasswordLength]
		[DataType(DataType.Password)]
        [DisplayName("Nytt lösenord")]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Bekräfta nytt lösenord")]
		public string ConfirmPassword { get; set; }
	}

	public class LogOnModel {
		[Required]
		[DisplayName("Användarnamn")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Lösenord")]
		public string Password { get; set; }

		[DisplayName("Kom ihåg mig?")]
		public bool RememberMe { get; set; }
	}

	[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Lösenordet och det bekräftade lösenorden måste vara lika.")]
	public class RegisterModel {
		[Required]
		[DisplayName("Användarnamn")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[DisplayName("E-post adress")]
		public string Email { get; set; }

		[Required]
		[ValidatePasswordLength]
		[DataType(DataType.Password)]
		[DisplayName("Lösenord")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Bekräfta lösenord")]
		public string ConfirmPassword { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class PropertiesMustMatchAttribute : ValidationAttribute {
		private const string defaultErrorMessage = "'{0}' och '{1}' är inte lika.";

		private readonly object typeId = new object();

		public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
			: base(defaultErrorMessage) {
			OriginalProperty = originalProperty;
			ConfirmProperty = confirmProperty;
		}

		public string ConfirmProperty { get; private set; }

		public string OriginalProperty { get; private set; }

		public override object TypeId {
			get { return typeId; }
		}

		public override string FormatErrorMessage(string name) {
			return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
				OriginalProperty, ConfirmProperty);
		}

		public override bool IsValid(object value) {
			var properties = TypeDescriptor.GetProperties(value);
			var originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
			var confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
			return Equals(originalValue, confirmValue);
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ValidatePasswordLengthAttribute : ValidationAttribute {
		private const string defaultErrorMessage = "'{0}' måste vara minst {1} bokstäver långt.";

		private readonly int minCharacters = Membership.Provider.MinRequiredPasswordLength;

		public ValidatePasswordLengthAttribute()
			: base(defaultErrorMessage) {}

		public override string FormatErrorMessage(string name) {
			return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
				name, minCharacters);
		}

		public override bool IsValid(object value) {
			var valueAsString = value as string;
			return (valueAsString != null && valueAsString.Length >= minCharacters);
		}
	}
}