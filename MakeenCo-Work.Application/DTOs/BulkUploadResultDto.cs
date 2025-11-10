namespace MakeenCo_Work.Application.DTOs
{
	public class BulkUploadResultDto
	{
		public int TotalRecords { get; set; }

		public int SuccessCount { get; set; }

		public int FailureCount { get; set; }

		public List<string> Errors { get; set; } = new List<string>();

		public List<BulkUserDto> FailedRecordes { get; set; } = new List<BulkUserDto>();
	}
}

