namespace MakeenCo_Work.Application.Command.User
{
	public class UserAuthCommand
	{
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string NationalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}

