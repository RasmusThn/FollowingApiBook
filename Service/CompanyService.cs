using AutoMapper;
using Contracts;
using Entites.Exceptions;
using Entites.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();

        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

        return companyToReturn;
    }

    public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
        {
            throw new CompanyCollectionBadRequest();
        }
        var companyEntites = _mapper.Map<IEnumerable<Company>>(companyCollection);

        foreach (var company in companyEntites)
        {
            _repository.Company.CreateCompany(company);
        }
        _repository.Save();

        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntites);
        var ids = string.Join(",",companyCollectionToReturn.Select(company => company.Id));

        return (companies: companyCollectionToReturn, ids:ids);
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {

        var companies = _repository.Company.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;

    }

    public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackchanges)
    {
        if (ids == null)
        {
            throw new IdParametersBadRequestException();
        }

        var companyEntitys = _repository.Company.GetByIds(ids, trackchanges);

        if (ids.Count() != companyEntitys.Count())
        {
            throw new CollectionByIdsBadRequestException();
        }

        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntitys);
        return companiesToReturn;
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(id, trackChanges);

        if (company is null)
        {
            throw new CompanyNotFoundException(id);
        }

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;

    }
}
