using System;
using System.Globalization;
using System.Xml.Serialization;

namespace EcbSdmx.Core.Domain.Response
{
    [Serializable]
    [XmlRoot(ElementName = "ObsValue", Namespace = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic")]
    public class ObservationValue
    {
        [XmlIgnore]
        public double? Value { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string ValueFormatted
        {
            get => Value.ToString();
            set => Value = SetValueAsDouble(value);
        }

        private double? SetValueAsDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result);
            if (double.IsNaN(result))
            {
                return null;
            }

            return result;
        }
    }
}
