using BurqAuthRestSharp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

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
        [Route("Test")]
        public IActionResult TestCreds([FromBody] JObject formData)
        {
            string appName = formData["app"].ToString();
            return Ok(new { message = AuthorizationValidator.Validate(appName, formData) });
        }
    }
}