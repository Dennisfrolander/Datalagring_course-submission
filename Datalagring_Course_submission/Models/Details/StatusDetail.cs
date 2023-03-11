
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Datalagring_Course_submission.Models.Details;

internal class StatusDetail
{
	public int Id { get; set; }

	public string CurrentStatus { get; set; } = null!;

}
