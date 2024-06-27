using System.Security.Claims;
using WarehouseAPI.Domain.Authentication.User;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface IHttpService
    {
        Requester GetRequester(ClaimsIdentity userIdentity);
    }
}
