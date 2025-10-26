using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class User
	{
		public Guid Id { get; private set; }

		[Required , MaxLength(100)]
		public string FirstName { get; private set; }


		[Required , MaxLength(100)]
		public string LastName { get; private set; }


		public string FullName => $"{FirstName} {LastName}";


        [Required , MaxLength(200) , EmailAddress]
		public string Email { get; private set; }


		[Required , MaxLength(20)]
		public string PhoneNumber { get; private set; }


		[MaxLength(50)]
		public string? Username { get; private set; }


		[Required]
		public string Password { get; private set; }


		public string? ProfilePictureUrl { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public DateTime? LastLoginAt { get; private set; }

		public Guid RoleId { get; private set; }

		public Role Role { get; private set; } = null!;


		public ICollection<Message> SentMessages { get; private set; } = new List<Message>();
		public ICollection<Message> ReceivedMessages { get; private set; } = new List<Message>();
		public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();



		private User() { }


		public User(string firstName , string lastName , string email , string phoneNumber , string password , Guid roleId)
		{
			Id = Guid.NewGuid();
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Password = password;
			RoleId = roleId;
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
			SentMessages = new List<Message>();
			ReceivedMessages = new List<Message>();
			Reservations = new List<Reservation>();
		}

		public void UpdateProfile(string firstName , string lastName , string phonenNumber , string? username = null)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phonenNumber;
			Username = username;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdatePassword(string password)
		{
			Password = password;
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

