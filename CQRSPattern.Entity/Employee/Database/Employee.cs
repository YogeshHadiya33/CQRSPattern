using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSPattern.Entity.Employee.Database;

[Table("CQRS_Employee")]
public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    public string Address { get; set; }

    [Required]
    [MaxLength(10)]
    public string Phone { get; set; }

    public string Department { get; set; }

    public string Designation { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}