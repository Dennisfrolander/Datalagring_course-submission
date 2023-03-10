namespace Datalagring_Course_submission.Models.Forms;

internal class OwnerForm
{
	public int Id { get; set; } 
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? PhoneNumber { get; set; }
	public string StreetName { get; set; } = null!;
	public string PostalCode { get; set; } = null!;
	public string City { get; set; } = null!;

	public string FullName()
	{
		string FullName = FirstName + " " + LastName;
		return FullName;
	}

}
