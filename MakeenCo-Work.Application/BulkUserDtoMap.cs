using CsvHelper.Configuration;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application
{
	public sealed class BulkUserDtoMap : ClassMap<BulkUserDto>
	{
		public BulkUserDtoMap()
		{
			Map(m => m.FirstName).Name("FirstName");
			Map(m => m.LastName).Name("LastName");
			Map(m => m.NationalCode).Name("NationalCode");
			Map(m => m.PhoneNumber).Name("PhoneNumber");
		}
	}
}

