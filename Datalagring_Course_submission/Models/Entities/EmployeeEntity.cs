using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

internal class EmployeeEntity
{
	[Key]
	public int Id { get; set; }
	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string FirstName { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string LastName { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string Email { get; set; } = null!;

	[Required]
	[Column(TypeName = "char(13)")]
	public string PhoneNumber { get; set; } = null!;

	[Required]
	public int PositionId { get; set; }
	public PositionEntity Position { get; set; } = null!;

	public ICollection<CommentEntity> Comments { get; } = new HashSet<CommentEntity>();

}
