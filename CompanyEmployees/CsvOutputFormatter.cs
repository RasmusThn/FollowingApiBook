using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace CompanyEmployees;

//TODO: Vi lade till stöd för EmployeeDto som ett riktigt fulhack.
//Skriv om och snygga till! Gör kanske generisk?
public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }



    protected override bool CanWriteType(Type? type)
    {
        if (typeof(CompanyDto).IsAssignableFrom(type)
        || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type)
        || typeof(EmployeeDto).IsAssignableFrom(type)
        || typeof(IEnumerable<EmployeeDto>).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }
        return false;



    }
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();



        if (context.Object is IEnumerable<CompanyDto>)
        {
            foreach (var company in (IEnumerable<CompanyDto>)context.Object)
            {
                FormatCompanyDtoCsv(buffer, company);
            }
        }
        else if (context.Object is CompanyDto)
        {
            FormatCompanyDtoCsv(buffer, (CompanyDto)context.Object);
        }
        else if (context.Object is IEnumerable<EmployeeDto>)
        {
            foreach (var employee in (IEnumerable<EmployeeDto>)context.Object)
            {
                FormatEmployeeDtoCsv(buffer, employee);
            }
        }
        else if (context.Object is EmployeeDto)
        {
            FormatEmployeeDtoCsv(buffer, (EmployeeDto)context.Object);
        }

        await response.WriteAsync(buffer.ToString());
    }
    private void FormatEmployeeDtoCsv(StringBuilder buffer, EmployeeDto employee)
    {
        buffer.AppendLine($"{employee.Id},\"{employee.Name}\",\"{employee.Age}\",\"{employee.Position}\"");
    }
    private void FormatCompanyDtoCsv(StringBuilder buffer, CompanyDto company)
    {
        buffer.AppendLine($"{company.Id},\"{company.Name}\",\"{company.FullAddress}\"");
    }
}