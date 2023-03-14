
namespace Datalagring_Course_submission.Models.Details;

internal class IssueDetails
{
	public Guid IssueNumber { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? PhoneNumber { get; set; }

	public DateTime CreatedDate { get; set; }
	
	public DateTime DueDate { get; set; }

	public string CurrentStatus { get; set; } = null!;

	public string Description { get; set; } = null!;

	public int StatusId { get; set; }

	public List<CommentDetails>? CommentList { get; set; }
}
