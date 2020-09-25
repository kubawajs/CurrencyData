using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CurrencyData.Api.Extensions
{
    public static class ObjectExtensions
    {
        public static byte[] ToByteArray<T>(this T obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            
            return ms.ToArray();
        }

        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default;

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(data);
            var obj = bf.Deserialize(ms);
            
            return (T)obj;
        }
    }
}
