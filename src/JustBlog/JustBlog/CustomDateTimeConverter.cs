using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace JustBlog
{
  /// <summary>
  /// This custom converter is used by Newtonsoft.Json to serialize dates in MM/dd/yyyy format.
  /// </summary>
  public class CustomDateTimeConverter : DateTimeConverterBase
  {
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(value.ToString());
    }
  }
}
