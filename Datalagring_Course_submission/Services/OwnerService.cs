

using Datalagring_Course_submission.Contexts;
using Datalagring_Course_submission.Models.Entities;
using Datalagring_Course_submission.Models.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Datalagring_Course_submission.Services;

internal class OwnerService
{
	private readonly static DataContext _context = new DataContext();

	public static async Task SaveOwnerAsync(OwnerForm newOwner)
	{
		var newOwnerEntity = new OwnerEntity
		{
			FirstName = newOwner.FirstName,
			LastName = newOwner.LastName,
			Email = newOwner.Email,
			PhoneNumber = newOwner.PhoneNumber,
		};

		var adressEntity = await _context.Adresses.SingleOrDefaultAsync
		(
			addresses => addresses.StreetName == newOwner.StreetName &&
			addresses.PostalCode == newOwner.PostalCode &&
			addresses.City == newOwner.City
		);

		if (adressEntity != null)
		{
			newOwnerEntity.AdressId = adressEntity.Id;
		}
		else
		{
			newOwnerEntity.Adress = new AddressEntity
			{
				StreetName = newOwner.StreetName,
				PostalCode = newOwner.PostalCode,
				City = newOwner.City,
			};
		}
		_context.Add(newOwnerEntity);
		await _context.SaveChangesAsync();
	}

	public static async Task<IEnumerable<OwnerForm>> GetAllOwnersAsync()
	{
		var owners = new List<OwnerForm>();
		foreach (var owner in await _context.Owners.Include(x => x.Adress).ToListAsync())
			owners.Add(new OwnerForm
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
			return owners;
		else
			return null!;
	}

	public static async Task<IEnumerable<OwnerForm>> GetSearchedOwnerByName(string searchName)
	{
		var owners = new List<OwnerForm>();
		foreach (var owner in await _context.Owners.Where(owner => owner.FirstName == searchName || owner.LastName == searchName).Include(x => x.Adress).ToListAsync())
			owners.Add(new OwnerForm
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

	public static async Task<OwnerForm> GetSearchedOwnerByEmail(string email)
	{ 
		var owner = await _context.Owners.Include(x => x.Adress).FirstOrDefaultAsync(x => x.Email == email);
		if (owner != null)
			return new OwnerForm
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
