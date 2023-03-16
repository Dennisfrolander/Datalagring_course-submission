using Datalagring_Course_submission.Contexts;
using Datalagring_Course_submission.Models.Entities;
using Datalagring_Course_submission.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Course_submission.Services;

internal class CommentService
{
	private readonly static DataContext _context = new DataContext();


	public static async Task SaveCommentAsync(CommentForm newComment)
	{

		var findEmploye = await _context.Employees.FirstOrDefaultAsync(employee => employee.Email == newComment.EmployeeEmail);
		if(findEmploye != null)
		{
			var newCommentEntity = new CommentEntity
			{
				Description = newComment.Description,
				CreatedDate = newComment.CreatedDate,
				IssueId = newComment.IssueNumber,
				EmployeeId = findEmploye.Id
			};

			_context.Add(newCommentEntity);
			await _context.SaveChangesAsync();
		}
	}
}
