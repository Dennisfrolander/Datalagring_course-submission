

using Datalagring_Course_submission.Contexts;
using Datalagring_Course_submission.Models.Entities;
using Datalagring_Course_submission.Models.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Datalagring_Course_submission.Services;

internal class OwnerService
{
	private readonly static DataContext _context = new DataContext();

	public static async Task SaveOwnerAsync(RegistrationOwnerForm registrationOwnerForm)
	{
		var ownerEntity = new OwnerEntity
		{
			FirstName = registrationOwnerForm.FirstName,
			LastName = registrationOwnerForm.LastName,
			Email = registrationOwnerForm.Email,
			PhoneNumber = registrationOwnerForm.PhoneNumber,
		};

		var adressEntity = await _context.Adresses.SingleOrDefaultAsync
		(
			addresses => addresses.StreetName == registrationOwnerForm.StreetName &&
			addresses.PostalCode == registrationOwnerForm.PostalCode &&
			addresses.City == registrationOwnerForm.City
		);

		if (adressEntity != null)
		{
			ownerEntity.AdressId = adressEntity.Id;
		}
		else
		{
			ownerEntity.Adress = new AddressEntity
			{
				StreetName = registrationOwnerForm.StreetName,
				PostalCode = registrationOwnerForm.PostalCode,
				City = registrationOwnerForm.City,
			};
		}
		_context.Add(ownerEntity);
		await _context.SaveChangesAsync();
	}

	public static async Task<IEnumerable<RegistrationOwnerForm>> GetAllOwnersAsync()
	{
		var owners = new List<RegistrationOwnerForm>();
		foreach (var owner in await _context.Owners.Include(x => x.Adress).ToListAsync())
			owners.Add(new RegistrationOwnerForm
			{
				FirstName = owner.FirstName,
				LastName = owner.LastName,
				Email = owner.Email,
				PhoneNumber = owner.PhoneNumber,
				StreetName = owner.Adress.StreetName,
				PostalCode = owner.Adress.PostalCode,
				City = owner.Adress.City,

			});
		if (owners.Any())
		{
			return owners;
		}
		else
		{
			return null!;
		}
	}

	public static async Task<IEnumerable<RegistrationOwnerForm>> GetSearchedOwnerByName(string searchName)
	{
		var owners = new List<RegistrationOwnerForm>();
		foreach (var owner in await _context.Owners.Where(owner => owner.FirstName == searchName || owner.LastName == searchName).Include(x => x.Adress).ToListAsync())
			owners.Add(new RegistrationOwnerForm
			{
				FirstName = owner.FirstName,
				LastName = owner.LastName,
				Email = owner.Email,
				PhoneNumber = owner.PhoneNumber,
				StreetName = owner.Adress.StreetName,
				PostalCode = owner.Adress.PostalCode,
				City = owner.Adress.City,

			});
		if (owners.Any())
		{
			return owners;
		}
		else
		{
			return null!;
		}
	}

	public static async Task<RegistrationOwnerForm> GetSearchedOwnerByEmail(string email)
	{ 
		var owner = await _context.Owners.Include(x => x.Adress).FirstOrDefaultAsync(x => x.Email == email);
		if (owner != null)
			return new RegistrationOwnerForm
			{
				FirstName = owner.FirstName,
				LastName = owner.LastName,
				Email = owner.Email,
				PhoneNumber = owner.PhoneNumber,
				StreetName = owner.Adress.StreetName,
				PostalCode = owner.Adress.PostalCode,
				City = owner.Adress.City,
			};
		else
		{
			return null!;
		}
	}

	public static async Task DeleteOwnerAsync(string email)
	{
		var owner = await _context.Owners.FirstOrDefaultAsync(x => x.Email == email);

		if(owner != null)
		{
			_context.Remove(owner);
			await _context.SaveChangesAsync();
		}
	}
}
