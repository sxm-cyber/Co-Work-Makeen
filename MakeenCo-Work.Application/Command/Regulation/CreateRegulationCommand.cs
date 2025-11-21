using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Application.Command
{
    public class CreateRegulationCommand
    {

        public string Title { get; set; } 
        public string Content { get; set; } 
        public bool IsActive { get; set; }
    }

}

