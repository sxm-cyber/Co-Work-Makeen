namespace MakeenCo_Work.Application.DTOs
{
    public class SpaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public string? Location { get; set; }
    }
}
