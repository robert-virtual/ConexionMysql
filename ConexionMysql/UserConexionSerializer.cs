using Microsoft.Toolkit.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionMysql
{
    public class UserConexionSerializer : IObjectSerializer
    {
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings(); 
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }

        public string Serialize<T>(T value)
        {

            return JsonConvert.SerializeObject(value, typeof(T), Formatting.Indented,settings);    
        }
    }
}
