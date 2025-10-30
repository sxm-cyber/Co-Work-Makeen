using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MakeenCo_Work.Domain.Models
{
	public class User : IdentityUser<Guid>
	{

		[Required , MaxLength(100)]
		public string FirstName { get; private set; }


		[Required , MaxLength(100)]
		public string LastName { get; private set; }

		public string FullName => $"{FirstName} {LastName}";

		public string? ProfilePictureUrl { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public DateTime? LastLoginAt { get; private set; }


		public ICollection<Message> SentMessages { get; private set; } = new List<Message>();
		public ICollection<Message> ReceivedMessages { get; private set; } = new List<Message>();
		public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();



		private User() { }


		public User(string firstName , string lastName , string email , string phoneNumber)
		{
			Id = Guid.NewGuid();
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
			SentMessages = new List<Message>();
			ReceivedMessages = new List<Message>();
			Reservations = new List<Reservation>();
		}

		public void UpdateProfile(string firstName , string lastName , string phonenNumber)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phonenNumber;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateProfilePicture(string profilePictureUrl)
		{
			ProfilePictureUrl = profilePictureUrl;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateLastLogin()
		{
			LastLoginAt = DateTime.UtcNow;
		}
	}
}

