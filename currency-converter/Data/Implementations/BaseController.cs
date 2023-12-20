using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Data.Implementations
{
    public class BaseController : Controller
    {
        protected bool Admin()
        {
            return User.Claims.First(claim => claim.Type.Contains("role")).Value == "Admin";
        }
        protected string Email()
        {
            return User.Claims.First(claim => claim.Type.Contains("email")).Value;
        }
        protected string Role()
        {
            return User.Claims.First(claim => claim.Type.Contains("role")).Value;
        }
        protected int UserId()
        {
            return Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
        }
    }
}
