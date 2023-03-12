namespace Datalagring_Course_submission.Models.Forms;

internal class CommentForm
{
	public string Description { get; set; } = null!;
	public DateTime CreatedDate { get; set; } = DateTime.Now;
	public string EmployeeEmail { get; set; } = null!;
	public Guid IssueNumber { get; set; }
}
