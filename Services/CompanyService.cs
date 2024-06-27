using System.Data;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces.Repositories;
using WarehouseAPI.Domain.Interfaces.Services;
using WarehouseAPI.Domain.Models;
using WarehouseAPI.Domain.Views;
using WarehouseAPI.Infra.Repositories;

namespace WarehouseAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private IRepository<Company> _companyRepository;

        public CompanyService()
        {
        }

        public CompanyService(IRepository<Company> repository)
        {
            _companyRepository = repository;
        }

        public CompanyView Create(CompanyModel company)
        {
            Company newCompany = new Company(company);

            _companyRepository.Create(newCompany);
            _companyRepository.SaveChanges();

            CompanyView companyView = new CompanyView(newCompany);

            return companyView;
        }

        public CompanyView Delete(int Id)
        {
            Company companyToDelete = _companyRepository.FindById(Id);

            if(companyToDelete == null)
            {
                return null;
            }

            _companyRepository.Delete(companyToDelete);
            _companyRepository.SaveChanges();
                
            return new CompanyView(companyToDelete);
        }

        public CompanyView FindById(int id)
        {
            Company company = _companyRepository.FindById(id);

            if(company is null)
            {
                return null;
            }

            CompanyView companyView = new CompanyView(company);

            return companyView;
        }

        public CompanyView Update(CompanyModel companyModel)
        {
            Company company = _companyRepository.FindById(companyModel.Id);

            if(company == null)
            {
                return null;
            }

            company.Name = companyModel.Name;

            _companyRepository.Update(company);
            _companyRepository.SaveChanges();

            return new CompanyView(company);
        }
    }
}
