using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

internal class PositionEntity
{
	[Key]
	public int Id { get; set; }
	[Required]
	[Column(TypeName = "nvarchar(50)")]
	public string Title { get; set; } = null!;

	public ICollection<EmployeeEntity> Employees { get; } = new HashSet<EmployeeEntity>();
}
