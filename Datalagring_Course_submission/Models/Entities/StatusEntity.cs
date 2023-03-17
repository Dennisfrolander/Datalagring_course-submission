using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

internal class StatusEntity
{
	[Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string CurrentStatus { get; set; } = null!;

	public ICollection<IssueEntity> Issues { get; set; } = new HashSet<IssueEntity>();

}
