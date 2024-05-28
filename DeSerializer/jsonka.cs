using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeSerializer
{
    public static class jsonka
    {
        public static T deser<T>(string path)
        {
            if (!File.Exists(path)) File.WriteAllText(path, "");
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static void serial<T>(string path, T obj) => File.WriteAllText(path,JsonConvert.SerializeObject(obj));
    }
}
