using System;
using System.Windows.Markup;

namespace EncryptionApp.Extensions
{
	public class EnumBindingSourceExtension : MarkupExtension
	{
		public Type EnumType { get; set; }

		public EnumBindingSourceExtension(Type enumType)
		{
			if (enumType == null || !enumType.IsEnum)
				throw new ArgumentException($"Argument {nameof(enumType)} cannot be null and must be an enum.");

			EnumType = enumType;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Enum.GetValues(EnumType);
		}
	}
}