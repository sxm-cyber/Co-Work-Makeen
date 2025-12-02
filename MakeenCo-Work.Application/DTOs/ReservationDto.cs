namespace MakeenCo_Work.Application.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string ReservationNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
    }
}
