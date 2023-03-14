using Datalagring_Course_submission.Contexts;
using Datalagring_Course_submission.Models.Details;
using Datalagring_Course_submission.Models.Entities;
using Datalagring_Course_submission.Models.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.InteropServices;

namespace Datalagring_Course_submission.Services;

internal class EmployeeService
{
	private readonly static DataContext _context = new DataContext();

	public static async Task SaveEmployeeAsync(EmployeeForm newEmployee)
	{
		var newEmployeeEntity = new EmployeeEntity
		{
			FirstName= newEmployee.FirstName,
			LastName= newEmployee.LastName,
			PhoneNumber= newEmployee.PhoneNumber,
			Email = newEmployee.Email,
		};


		var positionEntity = await _context.Positions.SingleOrDefaultAsync(position => position.Id == newEmployee.JobId);

		if (positionEntity != null )
		{
			newEmployeeEntity.PositionId = positionEntity.Id;
			_context.Add(newEmployeeEntity);
			await _context.SaveChangesAsync();
		}

	}

	public static async Task <IEnumerable<EmployeeForm>> GetAllEmployees()
	{
		var employees = new List<EmployeeForm>();

		foreach (var employee in await _context.Employees.Include(x => x.Position).ToListAsync())
			employees.Add(new EmployeeForm
			{
				FirstName= employee.FirstName,
				LastName= employee.LastName,
				PhoneNumber= employee.PhoneNumber,
				Email= employee.Email,
				JobTitle = employee.Position.Title
			});
		if (employees.Any())
			return employees;
		else
			return null!;
	}

	public static async Task<PositionDetail> GetSearchedPositionById(int id)
	{
		var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
		if(position != null )
			return new PositionDetail
			{ 
				Id = id, 
				Title = position.Title
			};
		else 
			return null!;
	}

	public static async Task<EmployeeDetails> GetSearchedEmployeeByEmail(string email)
	{
		var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
		if (employee != null)
			return new EmployeeDetails
			{
				Id = employee.Id,
				FirstName = employee.FirstName,
				LastName = employee.LastName,
				PhoneNumber = employee.PhoneNumber,
				Email = employee.Email
			};
		else return null!;
	}
}
