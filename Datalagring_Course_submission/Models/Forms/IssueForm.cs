
namespace Datalagring_Course_submission.Models.Forms;

internal class IssueForm
{
	public Guid IssueNumber { get; set; } = Guid.NewGuid();

	public string Description { get; set; } = null!;

	public DateTime CreatedDate { get; set; } = DateTime.Now;

	public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);

	public string OwnerEmail { get; set; } = null!;

	public int StatusId { get; set; }
}
