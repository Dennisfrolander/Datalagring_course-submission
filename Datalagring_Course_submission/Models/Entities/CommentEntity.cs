using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

internal class CommentEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(500)")]
	public string Description { get; set; } = null!;

	[Required]
	[Column(TypeName = "datetime")]
	public DateTime CreatedDate { get; set; }

	[Required]
	public Guid IssueId { get; set; }
	public IssueEntity Issue { get; set; } = null!;

	[Required]
	public int EmployeeId { get; set; }
	public EmployeeEntity Employee { get; set; } = null!;

}
