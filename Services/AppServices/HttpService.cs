using System.Data;
using System.Security.Claims;
using System.Xml.Linq;
using WarehouseAPI.Domain.Authentication.User;
using WarehouseAPI.Domain.Interfaces.Services;

namespace WarehouseAPI.Services.AppServices
{
    public class HttpService : IHttpService
    {

        public HttpService()
        {
        }

        public Requester GetRequester(ClaimsIdentity userIdentity)
        {
            string userId = userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            string companiesId = userIdentity.FindFirst(ClaimTypes.UserData).Value;
            string name= userIdentity.FindFirst(ClaimTypes.Name).Value;

            return new Requester { 
                Id =  userId,
                Name = name, 
                CompaniesId = companiesId };
        }
    }
}