using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MakeenCo_Work.Domain.Models
{
	public class Role : IdentityRole<Guid>
	{

		[MaxLength(200)]
		public string? Description { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		private Role() { }


		public Role(string name , string? description = null)
		{
			Id = Guid.NewGuid();
			Name = name;
			Description = description;
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
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

