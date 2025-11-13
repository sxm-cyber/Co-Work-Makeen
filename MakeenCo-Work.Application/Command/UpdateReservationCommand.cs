namespace MakeenCo_Work.API.Commands
{
    public class UpdateReservationCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
    }
}
