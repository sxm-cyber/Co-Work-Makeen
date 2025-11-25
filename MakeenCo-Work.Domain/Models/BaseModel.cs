namespace MakeenCo_Work.Domain.Models
{
	public abstract class BaseModel
	{
		public Guid Id { get; protected set; }

		public DateTime CreatedAt { get; protected set; }

		public DateTime UpdatedAt { get; protected set; }


		protected BaseModel()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}


		protected void UpdateTimestamp()
		{
			UpdatedAt = DateTime.UtcNow;
		}
	}
}

