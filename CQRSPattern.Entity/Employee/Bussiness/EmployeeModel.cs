namespace CQRSPattern.Entity.Employee.Bussiness;

public class EmployeeModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedOn { get; set; }
}