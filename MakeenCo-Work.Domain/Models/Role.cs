using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class Role
	{
		public Guid Id { get; private set; }


		[Required , MaxLength(50)]
		public string Name { get; private set; }


		[MaxLength(200)]
		public string? Description { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public ICollection<User> Users { get; private set; } = new List<User>();

		private Role() { }


		public Role(string name , string? description = null)
		{
			Id = Guid.NewGuid();
			Name = name;
			Description = description;
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
			Users = new List<User>();
		}

		public void Update(string name , string? description = null)
		{
			Name = name;
			Description = description;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
		}
	}
}

