using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Supperxin.SendCloud.Utility
{
    public class JsonBuilder
    {
        public static void BuildDirectly(JObject jObj, object objToBuild, string key,  string keyAppend = "")
        {
            var section = JToken.Parse(JsonConvert.SerializeObject(objToBuild));
            jObj.Add($"{keyAppend}{key}{keyAppend}", section);
        }

        public static void BuildWithToString(JObject jObj, IEnumerable<object> objToBuild, string key,  string keyAppend = "")
        {
            var stringList = new List<string> ();
            foreach(var obj in objToBuild)
            {
                stringList.Add(obj.ToString());
            }

            BuildDirectly(jObj, stringList, key, keyAppend);
        }
    }
}