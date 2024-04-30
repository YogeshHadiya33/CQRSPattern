namespace CQRSPattern.Entity.Employee.Bussiness;

public class CreateEmployeeModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }
}