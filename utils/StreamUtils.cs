using KinesisListener.exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KinesisListener.utils
{
    public static class StreamUtils
    {
        
        public static T GetStreamRecord<T>(MemoryStream stream)
        {
            try
            {
                //var serializer = new JsonSerializer();

                using (var sr = new StreamReader(stream, Encoding.ASCII))
                    //using (var jsonTextReader = new JsonTextReader(sr))
                    {
                    var streamContent = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(streamContent); //serializer.Deserialize<T>(jsonTextReader);
                }
            }
            catch (Exception ex)
            {
                throw new BatchProcessingException(ex.Message, ex);
            }
        }
    }
}
