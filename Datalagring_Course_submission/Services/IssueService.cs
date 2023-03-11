
using Datalagring_Course_submission.Contexts;
using Datalagring_Course_submission.Models.Details;
using Datalagring_Course_submission.Models.Entities;
using Datalagring_Course_submission.Models.Forms;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Course_submission.Services;

internal class IssueService
{
	private readonly static DataContext _context = new DataContext();

	public static async Task SaveIssueAsync(IssueForm newIssue)
	{
		var ownerEntity = await _context.Owners.SingleOrDefaultAsync(owner => owner.Email == newIssue.OwnerEmail);
		var newIssueEntity = new IssueEntity
		{
			IssueNumber = newIssue.IssueNumber,
			Description = newIssue.Description,
			CreatedDate = newIssue.CreatedDate,
			DueDate = newIssue.DueDate,
			StatusId = 1,
		};
		

		if(ownerEntity != null)
		{
			newIssueEntity.IssueNumber = newIssue.IssueNumber;
			newIssueEntity.OwnerId = ownerEntity.Id;
			_context.Add(newIssueEntity);
			await _context.SaveChangesAsync();
		}
	}

	public static async Task<IEnumerable<IssueDetails>> GetAllIssuesAsync()
	{
		var issues = new List<IssueDetails>();
		foreach (var issue in await _context.Issues.Include(x => x.Owner).Include(x => x.Status).ToListAsync())
			issues.Add(new IssueDetails
			{
				IssueNumber = issue.IssueNumber,
				FirstName = issue.Owner.FirstName,
				LastName = issue.Owner.LastName,
				Email = issue.Owner.Email,
				PhoneNumber = issue.Owner.PhoneNumber,
				CreatedDate = issue.CreatedDate,
				DueDate = issue.DueDate,
				CurrentStatus = issue.Status.CurrentStatus,
				Description = issue.Description,
			});
		if (issues.Any())
			return issues;
		else
			return null!;
	}

	public static async Task<IssueDetails> GetSearchedIssueByGuid(Guid searchIssueNumber)
	{
		var issue = await _context.Issues.Include(x => x.Owner).Include(x => x.Status).FirstOrDefaultAsync(x => x.IssueNumber == searchIssueNumber);
		if (issue != null)
			return new IssueDetails
			{
				IssueNumber = issue.IssueNumber,
				FirstName = issue.Owner.FirstName,
				LastName = issue.Owner.LastName,
				Email = issue.Owner.Email,
				PhoneNumber = issue.Owner.PhoneNumber,
				CreatedDate = issue.CreatedDate,
				DueDate = issue.DueDate,
				CurrentStatus = issue.Status.CurrentStatus,
				Description = issue.Description,
			};
		else return null!;
	}

	public static async Task UpdateIssueStatus(IssueDetails issue)
	{
		var issueEntity = await _context.Issues.FirstOrDefaultAsync(x => x.IssueNumber == issue.IssueNumber);
		if(issueEntity != null)
		{
			issueEntity.StatusId = issue.StatusId;
			_context.Update(issueEntity);
			await _context.SaveChangesAsync();
		}
	}

	public static async Task<StatusDetail> FindStatusWithString(string searchResult)
	{
		var status = await _context.Statuses.FirstOrDefaultAsync(x => x.CurrentStatus == searchResult);
		if (status != null)
			return new StatusDetail
			{
				Id = status.Id,
				CurrentStatus = status.CurrentStatus
			};
		else
			return null!;
	}

}
