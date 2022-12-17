using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utils.Serialization
{
    public class ZorgBinaryFormatter
    {
        private readonly BinaryFormatter _binaryFormatter;

        public ZorgBinaryFormatter()
        {
            _binaryFormatter = new BinaryFormatter
            {
                SurrogateSelector = ConstructSurrogateSelector()
            };
        }

        private SurrogateSelector ConstructSurrogateSelector()
        {
            var surrogateSelector = new SurrogateSelector();
            var v3Ss = new Vector3SerializationSurrogate();
            
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3Ss);

            return surrogateSelector;
        }

        public void Serialize(FileStream fileStream, object serializable)
        {
            _binaryFormatter.Serialize(fileStream, serializable);
        }

        public object Deserialize(FileStream fileStream)
        {
            return _binaryFormatter.Deserialize(fileStream);
        }
    }
}