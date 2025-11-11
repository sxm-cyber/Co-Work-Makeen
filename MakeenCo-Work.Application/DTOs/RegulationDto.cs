using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Application.DTOs
{
    public class RegulationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public bool IsActive { get; set; }
       
    }
}
