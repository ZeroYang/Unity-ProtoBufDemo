using UnityEngine;
using System.IO;
using System.Collections;
using ProtoBuf;

public class PackCodec{
    
    static public byte[] Serialize<T>(T msg)
    {
        byte[] result = null;
        if(msg != null){
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize<T>(stream, msg);
                result = stream.ToArray();
            }
        }
        return result;

    }

    static public T Deserialize<T>(byte[] msg)
    {
        T result = default(T);
        if(msg != null){
            using (var stream = new MemoryStream(msg))
            {
                result = Serializer.Deserialize<T>(stream);
            }
        }
        return result;
    }
}