using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

internal class IssueEntity
{
	[Key]
	[Column(TypeName = "uniqueidentifier")]
	public Guid IssueNumber { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(500)")]
	public string Description { get; set; } = null!;

	[Required]
	[Column(TypeName = "datetime")]
	public DateTime CreatedDate { get; set; }

	[Required]
	[Column(TypeName = "datetime")]
	public DateTime DueDate { get; set; }

	[Required]
	public int OwnerId { get; set; }
	public OwnerEntity Owner { get; set; } = null!;

	[Required]
	public int StatusId { get; set; }
	public StatusEntity Status { get; set; } = null!;

	public ICollection<CommentEntity> Comments { get; } = new HashSet<CommentEntity>();

}
