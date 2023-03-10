using Datalagring_Course_submission.Models.Forms;
using Microsoft.Identity.Client;

namespace Datalagring_Course_submission.Services;

internal class MenuService
{

	#region MainMenu
	public async Task MainMenuAsync()
	{
		bool mainMenuRun = true;
		do
		{
			Console.Clear();
			Console.WriteLine("Välj något av följande alternativ:");
			Console.WriteLine("1. Ägare \n2. Anställda \n3. Problem \n4. Avsluta. ");

			switch (Console.ReadLine())
			{
				case "1":
					await OwnerMenuAsync();
					break;
				case "2":
					await EmployeeMenuAsync();
					break;
				case "3":
					break;
				case "4":
					mainMenuRun = false;
					break;
			}
		}
		while (mainMenuRun);
	}
	#endregion

	#region OwnerSubMenu
	public async Task OwnerMenuAsync()
	{
		bool OwnerMenuRun = true;
		Console.Clear();
		Console.WriteLine("Välj något av följande alternativ:");
		Console.WriteLine("1. Skapa en ägare \n2. Få fram alla ägare \n3. Sök på en specifik ägare \n4. ta bort ägare \n5. Gå tillbaka. ");
		do
		{
			switch (Console.ReadLine())
			{
				case "1":
					Console.Clear();
					await CreateOwnerAsync();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();

					OwnerMenuRun = false;
					break;
				case "2":
					Console.Clear();
					await AllOwners();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();

					OwnerMenuRun = false;
					break;
				case "3":
					Console.Clear();
					await SpecificSearchedOwners();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();
					OwnerMenuRun = false;
					break;
				case "4":
					Console.Clear();
					await DeteleOwnerAsync();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();
					OwnerMenuRun = false;
					break;
				case "5":
					OwnerMenuRun = false;
					break;
			}
		}
		while (OwnerMenuRun);
	}
	
	public async Task CreateOwnerAsync()
	{
		var newRegistrationOwner = new OwnerForm();

		Console.WriteLine("Alla med en stjärna (*) måste fyllas i.");

		Console.Write("*Förnamn: ");
		newRegistrationOwner.FirstName = Console.ReadLine()!.Trim();

		Console.Write("*Efternamn: ");
		newRegistrationOwner.LastName = Console.ReadLine()!.Trim();

		Console.Write("*Email: ");
		newRegistrationOwner.Email = Console.ReadLine()!.Trim();

		Console.Write("Telefonnummer (max 13 siffror): ");
		newRegistrationOwner.PhoneNumber = Console.ReadLine()!.Trim() ?? "";

		Console.Write("*Gatuadress: ");
		newRegistrationOwner.StreetName = Console.ReadLine()!.Trim();

		Console.Write("*Postnummer (max 6 siffror): ");
		newRegistrationOwner.PostalCode = Console.ReadLine()!.Trim();

		Console.Write("*Stad: ");
		newRegistrationOwner.City = Console.ReadLine()!.Trim();

		var emailExist = await OwnerService.GetSearchedOwnerByEmail(newRegistrationOwner.Email);



		if (emailExist != null)
			Console.WriteLine("Din email finns redan registrerad i databasen, använd en ny email eller sök på användare");

		if (string.IsNullOrEmpty(newRegistrationOwner.FirstName))
			Console.WriteLine("Var vänlig att fyll i förnamn");

		if (string.IsNullOrEmpty(newRegistrationOwner.LastName))
			Console.WriteLine("Var vänlig att fyll i Efternamn");

		if (string.IsNullOrEmpty(newRegistrationOwner.Email))
			Console.WriteLine("Var vänlig att fyll i Email");

		if (string.IsNullOrEmpty(newRegistrationOwner.StreetName))
			Console.WriteLine("Var vänlig att fyll i Gatuadress");

		if (string.IsNullOrEmpty(newRegistrationOwner.PostalCode))
			Console.WriteLine("Var vänlig att fyll i Postnummer");

		if (string.IsNullOrEmpty(newRegistrationOwner.City))
			Console.WriteLine("Var vänlig att fyll i stad");

		if (!string.IsNullOrEmpty(newRegistrationOwner.FirstName) &&
			!string.IsNullOrEmpty(newRegistrationOwner.LastName) &&
			!string.IsNullOrEmpty(newRegistrationOwner.Email) &&
			!string.IsNullOrEmpty(newRegistrationOwner.StreetName) &&
			!string.IsNullOrEmpty(newRegistrationOwner.PostalCode) &&
			!string.IsNullOrEmpty(newRegistrationOwner.City) &&
			newRegistrationOwner.PostalCode.Length < 7 &&
			newRegistrationOwner.PhoneNumber.Length < 14 &&
			emailExist == null)
		{
			await OwnerService.SaveOwnerAsync(newRegistrationOwner);
			Console.WriteLine($"Grattis {newRegistrationOwner.FirstName} {newRegistrationOwner.LastName}! Du har nu skapat din användare!");
		}
		else
		{
			Console.WriteLine("Din användare skapades inte. tryck på valfri knapp för att fortsätta. Detta kan bero på att du inte fyllde i allt eller du skrev in något felaktigt.");
		}
	}

	public static async Task AllOwners()
	{
		var allOwners = await OwnerService.GetAllOwnersAsync();
		if (allOwners != null)
		{
			foreach (var owner in allOwners)
			{
				Console.WriteLine($"Namn: {owner.FullName()}");
				Console.WriteLine($"Email: {owner.Email}");
				if(owner.PhoneNumber == null)
					Console.WriteLine($"Telefon-nummer: {owner.PhoneNumber}");
				else
					Console.WriteLine($"Telefon-nummer: Inget tillagt");
				Console.WriteLine($"Adress: {owner.StreetName}, {owner.PostalCode},{owner.City}. \n");
			}
		}
		else
		{
			Console.WriteLine("Det finns inga ägare i registret än.");
		}
	}

	public static async Task SpecificSearchedOwners()
	{
		Console.WriteLine("Skriv in förnamnet eller efternamnet.");
		var userInput = Console.ReadLine();

		if(!string.IsNullOrEmpty(userInput))
		{
			var searchedOwners = await OwnerService.GetSearchedOwnerByName(userInput);
			if(searchedOwners != null)
			{
				Console.Clear();
				foreach (var owner in searchedOwners)
				{
					Console.WriteLine($"Namn: {owner.FullName()}");
					Console.WriteLine($"Email: {owner.Email}");
					if (owner.PhoneNumber == null)
						Console.WriteLine($"Telefon-nummer: {owner.PhoneNumber}");
					else
						Console.WriteLine($"Telefon-nummer: Inget tillagt");
					Console.WriteLine($"Adress: {owner.StreetName}, {owner.PostalCode},{owner.City}. \n");
				}
			}
			else
				Console.WriteLine($"Det fanns inga sökresultat på {userInput}");
		}
		else
			Console.WriteLine("Ett fel inträffade, försök igen senare.");
	}

	public static async Task DeteleOwnerAsync()
	{
		Console.WriteLine("Skriv in mailen på den ägaren du vill ta bort");
		var userInput = Console.ReadLine();
		if (!string.IsNullOrEmpty(userInput))
		{
			var owner = await OwnerService.GetSearchedOwnerByEmail(userInput);
			if (owner != null)
			{
				await OwnerService.DeleteOwnerAsync(userInput);
				Console.WriteLine($"Användaren {owner.FirstName} {owner.LastName} har tagits bort.");
			}
			else
				Console.WriteLine("Det finns inget ägare med den här mailen.");

		}
		else
			Console.WriteLine("Något fel inträffade, försök igen senare.");
	}
	#endregion

	#region EmployeeSubMenu
	public async Task EmployeeMenuAsync()
	{
		bool EmployeeMenuRun = true;
		Console.Clear();
		Console.WriteLine("Välj något av följande alternativ:");
		Console.WriteLine("1. Skapa en anställd \n2. Få fram alla anstälda \n3. Gå tillbaka. ");
		do
		{
			switch (Console.ReadLine())
			{
				case "1":
					Console.Clear();
					await CreateEmployeeAsync();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();

					EmployeeMenuRun = false;
					break;
				case "2":
					Console.Clear();
					await AllEmployees();
					Console.WriteLine("Tryck på valfri tanget för att gå tillbaka till huvudmenyn");
					Console.ReadKey();

					EmployeeMenuRun = false;
					break;
				case "3":
					EmployeeMenuRun = false;
					break;
			}
		}
		while (EmployeeMenuRun);
	}

	public async Task CreateEmployeeAsync()
	{
		var newEmployee = new EmployeeForm();
		Console.WriteLine("Vilken position ska den anställda ha?");
		Console.WriteLine("1. Property Manager \n2. Leasing Agent \n3. Asset Manager \n4. Maintenance Technician");
		Console.WriteLine("Skriv siffran på ett av ovanstående (1-4)");
		if (int.TryParse(Console.ReadLine(), out int jobId))
		{
			newEmployee.JobId = jobId;
			Console.WriteLine("Alla med en stjärna (*) måste fyllas i.");

			Console.Write("*Förnamn: ");
			newEmployee.FirstName = Console.ReadLine()!.Trim();

			Console.Write("*Efternamn: ");
			newEmployee.LastName = Console.ReadLine()!.Trim();

			Console.Write("*Email: ");
			newEmployee.Email = Console.ReadLine()!.Trim();

			Console.Write("Telefonnummer (max 13 siffror): ");
			newEmployee.PhoneNumber = Console.ReadLine()!.Trim();

			var JobIdExists = await EmployeeService.GetSearchedPositionById(newEmployee.JobId);

			if (JobIdExists == null)
				Console.WriteLine("Den position finns inte.");

			if (string.IsNullOrEmpty(newEmployee.FirstName))
				Console.WriteLine("Var vänlig att fyll i förnamn");

			if (string.IsNullOrEmpty(newEmployee.LastName))
				Console.WriteLine("Var vänlig att fyll i Efternamn");

			if (string.IsNullOrEmpty(newEmployee.Email))
				Console.WriteLine("Var vänlig att fyll i Email");

			if (string.IsNullOrEmpty(newEmployee.PhoneNumber))
				Console.WriteLine("Var vänlig att fyll i Telefon-nummer");

			if (!string.IsNullOrEmpty(newEmployee.FirstName) &&
				!string.IsNullOrEmpty(newEmployee.LastName) &&
				!string.IsNullOrEmpty(newEmployee.Email) &&
				!string.IsNullOrEmpty(newEmployee.PhoneNumber) &&
				newEmployee.PhoneNumber.Length < 14 &&
				JobIdExists != null)

			{
				await EmployeeService.SaveEmployeeAsync(newEmployee);
				Console.WriteLine($"Grattis {newEmployee.FirstName} {newEmployee.LastName}! Du är nu anställd som {JobIdExists.Title}!");
			}
			else
			{
				Console.WriteLine("Din anställd skapades inte. tryck på valfri knapp för att fortsätta. Detta kan bero på att du inte fyllde i allt eller du skrev in något felaktigt.");
			}
		}
		else
		{
			Console.WriteLine("Du måste skriva in en siffra");
		}




	}

	public static async Task AllEmployees()
	{
		var employees = await EmployeeService.GetAllEmployees();
		if (employees != null)
		{
			foreach (var employee in employees)
			{
				Console.WriteLine($"Namn: {employee.FullName()}");
				Console.WriteLine($"Email: {employee.Email}");
				Console.WriteLine($"Telenummer: {employee.PhoneNumber} \n");
			}
		}
		else
		{
			Console.WriteLine("Det finns inga anställda i företaget än.");
		}
	}

	#endregion
}
