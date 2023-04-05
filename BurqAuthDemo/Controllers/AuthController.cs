using BurqAuthRestSharp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BurqAuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetMetaData(string appName)
        {
            try
            {
                var metadata = AuthorizationMetaData.GetMetaData(appName);
                return Ok(metadata);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(400);
            }
        }

        [HttpPost]
        [Route("test")]
        public IActionResult TestCreds([FromBody] JObject formData)
        {
            Assembly assembly = Assembly.Load("BurqAuthRestSharp");

            // Load the class by its name
            string className = $"BurqAuthRestSharp.{formData["app"]}.{formData["app"]}Auth";
            Type type = assembly.GetType(className);

            if (type != null)
            {
                // Create an instance of the class
                object instance = JsonConvert.DeserializeObject(formData.ToString(), type);

                // Invoke a method on the instance
                string methodName = $"{formData["method"]}Async";
                MethodInfo methodInfo = type.GetMethod(methodName);

                if (methodInfo != null)
                {
                    var result = methodInfo.Invoke(instance, null);
                }
            }

            // Return a JSON response indicating success
            return Ok(new { message = "Credentials test successful" });
        }
    }
}
