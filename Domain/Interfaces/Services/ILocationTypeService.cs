using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface ILocationTypeService
    {
        ActionResult<List<LocationTypeView>> getAll();
    }
}
