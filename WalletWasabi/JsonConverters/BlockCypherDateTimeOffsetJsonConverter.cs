using Newtonsoft.Json;
using System;
using System.Globalization;

namespace WalletWasabi.JsonConverters
{
	public class BlockCypherDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
	{
		/// <inheritdoc />
		public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var value = reader.Value as string;

			if (string.IsNullOrWhiteSpace(value))
			{
				return DateTimeOffset.Now;
			}

			string time = value.Trim();
			return DateTimeOffset.Parse(time, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
		}

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
		}
	}
}
