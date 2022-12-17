using System.Runtime.Serialization;
using UnityEngine;

namespace Utils.Serialization
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector3) obj;
            
            info.AddValue("x", vector3.x);
            info.AddValue("y", vector3.y);
            info.AddValue("z", vector3.z);
        }

        public object SetObjectData(System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var result = (Vector3) obj;
            result.x = (float) info.GetValue("x", typeof(float));
            result.y = (float) info.GetValue("y", typeof(float));
            result.z = (float) info.GetValue("z", typeof(float));
            
            return result;
        }
    }
}