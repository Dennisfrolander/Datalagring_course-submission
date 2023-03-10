using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalagring_Course_submission.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class OwnerEntity
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

	[Column(TypeName = "char(13)")]
	public string? PhoneNumber { get; set; }

	[Required]
	public int AdressId { get; set; }
	public AddressEntity Adress { get; set; } = null!;

	public ICollection<IssueEntity> Issues { get; set; } = new HashSet<IssueEntity>();
}
