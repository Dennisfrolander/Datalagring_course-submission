namespace Datalagring_Course_submission.Models.Forms;

internal class EmployeeForm
{

	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string JobTitle { get; set; } = null!;
	public int JobId { get; set; }

	public string FullName()
	{
		string FullName = FirstName + " " + LastName;
		return FullName;
	}
}
