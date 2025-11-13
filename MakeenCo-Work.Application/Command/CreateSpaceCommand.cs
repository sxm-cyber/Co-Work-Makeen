namespace MakeenCo_Work.Application.Commands
{
    public class CreateSpaceCommand
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public string? Location { get; set; }
    }
}
