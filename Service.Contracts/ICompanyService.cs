using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    CompanyDto GetCompany(Guid companyId, bool trackChanges);
    CompanyDto CreateCompany(CompanyForCreationDto company);
    IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackchanges);
    (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companyCollection);

}
