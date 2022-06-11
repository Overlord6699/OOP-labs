using System.IO;
using System.Runtime.Serialization;

namespace UsefulScripts.NetScripts.Data
{
    public static class ByteConverter
    {
        public static byte[] SerializeObject(object obj)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(obj.GetType());
            serializer.WriteObject(stream, obj);

            
            return stream.ToArray();
        }

        public static byte[] SerializeObjectStruct(object obj)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(obj.GetType());
            serializer.WriteObject(stream, obj);
            
            return stream.ToArray();
        }

        public static byte[] SerializeObject<T>(object obj) where T : class
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            
            return stream.ToArray();
        }

        public static T DeserializeObject<T>(byte[] data) where T : class
        {
            var stream = new MemoryStream(data);
            var deserializer = new DataContractSerializer(typeof(T));
            
            return (T)deserializer.ReadObject(stream);
        }

        public static byte[] SerializeObjectStruct<T>(object obj) where T : struct
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            
            return stream.ToArray();
        }

        public static T DeserializeObjectStruct<T>(byte[] data) where T : struct
        {
            var stream = new MemoryStream(data);
            var deserializer = new DataContractSerializer(typeof(T));
            
            return (T)deserializer.ReadObject(stream);
        }
    }
}