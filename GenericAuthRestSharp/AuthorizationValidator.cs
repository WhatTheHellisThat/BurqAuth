using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace BurqAuthRestSharp
{
    public class AuthorizationValidator
    {
        public static string Validate(string appName, JObject json)
        {
            Assembly assembly = Assembly.Load("BurqAuthRestSharp");
            string result = "Ok";
            // Load the class by its name
            string className = $"BurqAuthRestSharp.{appName}Auth";
            Type type = assembly.GetType(className);

            if (type != null)
            {
                // Create an instance of the class
                object instance = JsonConvert.DeserializeObject(json.ToString(), type);

                // Invoke a method on the instance
                MethodInfo methodInfo = type.GetMethod($"{json["method"]}");

                if (methodInfo != null)
                {
                    dynamic methodResult = methodInfo.Invoke(instance, null);
                    result = methodResult;
                }
            }
            return result;
        }
    }
}