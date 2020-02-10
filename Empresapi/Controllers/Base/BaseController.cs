using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        //public string IPAddress => Request.HttpContext.Connection.RemoteIpAddress.ToString();
        //public string UserAgent => Request.Headers.ContainsKey("User-Agent") ? Request.Headers["User-Agent"].ToString() : "";

        public int? UserId => GetClaim(ClaimTypes.PrimarySid, out int userid) ? userid : (int?)null;
        public string Email => GetClaim(ClaimTypes.Email, out string email) ? email : null;

        //Use [Authorize(Roles = "")]
        public string Role => GetClaim(ClaimTypes.Role, out string role) ? role: null;


        private bool GetClaim<T>(string name, out T claim) where T : IConvertible
        {
            var strClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == name)?.Value;
            if (strClaim == null)
            {
                claim = default(T);
                return false;
            }
            try
            {
                claim = (T)Convert.ChangeType(strClaim, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                claim = default(T);
                return false;
            }
        }
        private T[] GetClaims<T>(string name) where T : IConvertible
        {
            var typedClaims = new List<T>();
            foreach (var claim in User.FindAll(name))
            {
                try
                {
                    typedClaims.Add((T)Convert.ChangeType(claim, typeof(T), CultureInfo.InvariantCulture));
                }
                catch(Exception e) 
                { 
                    var k = e; 
                }
            }

            return typedClaims.ToArray();
        }
    }
}