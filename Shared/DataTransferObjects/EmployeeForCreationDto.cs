namespace Shared.DataTransferObjects;

public record EmployeeForCreationDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }

}
