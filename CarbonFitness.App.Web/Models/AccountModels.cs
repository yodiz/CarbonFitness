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

	[PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
	public class ChangePasswordModel {
		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Current password")]
		public string OldPassword { get; set; }

		[Required]
		[ValidatePasswordLength]
		[DataType(DataType.Password)]
		[DisplayName("New password")]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm new password")]
		public string ConfirmPassword { get; set; }
	}

	public class LogOnModel {
		[Required]
		[DisplayName("User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Password")]
		public string Password { get; set; }

		[DisplayName("Remember me?")]
		public bool RememberMe { get; set; }
	}

	[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
	public class RegisterModel {
		[Required]
		[DisplayName("User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[DisplayName("Email address")]
		public string Email { get; set; }

		[Required]
		[ValidatePasswordLength]
		[DataType(DataType.Password)]
		[DisplayName("Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm password")]
		public string ConfirmPassword { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class PropertiesMustMatchAttribute : ValidationAttribute {
		private const string defaultErrorMessage = "'{0}' and '{1}' do not match.";

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
		private const string defaultErrorMessage = "'{0}' must be at least {1} characters long.";

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