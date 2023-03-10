using Datalagring_Course_submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace Datalagring_Course_submission.Contexts;

internal class DataContext : DbContext
{
	private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\denni\source\repos\datalagring\Property_Reporting_System\Datalagring_Course_submission\Contexts\Local_Property_Db.mdf;Integrated Security=True;Connect Timeout=30";


	#region constructors
	public DataContext()
	{
	}

	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	#endregion

	#region overrides
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseSqlServer(_connectionString);
	}

	#endregion

	#region entities
	public DbSet<AddressEntity> Adresses { get; set; } = null!;
	public DbSet<OwnerEntity> Owners { get; set; } = null!;
	public DbSet<PositionEntity> Positions { get; set; } = null!;
	public DbSet<EmployeeEntity> Employees { get; set; } = null!;
	public DbSet<IssueEntity> Issues { get; set; } = null!;
	public DbSet<StatusEntity> Statuses { get; set; } = null!;
	public DbSet<CommentEntity> Comments { get; set; } = null!;

	#endregion





}
