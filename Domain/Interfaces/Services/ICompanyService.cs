using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;

namespace WarehouseAPI.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        CompanyView FindById(int id);

        CompanyView Create(CompanyModel company);

        CompanyView Delete(int id);

        CompanyView Update(CompanyModel company);

    }
}
