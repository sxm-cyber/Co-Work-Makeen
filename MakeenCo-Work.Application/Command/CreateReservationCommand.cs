namespace MakeenCo_Work.Application.Commands
{
    public class CreateReservationCommand
    {
        public Guid UserId { get; set; }
        public Guid SpaceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public Guid? DiscountCodeId { get; set; }
    }
}
