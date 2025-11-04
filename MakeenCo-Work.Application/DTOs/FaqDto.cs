using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Application.DTOs
{
    public class FaqDto
    {
        public string Question { get;  set; }

        public string Answer { get;  set; }

        public bool IsActive { get; set; }

        public bool PublishInMainPage { get; set; }

        public bool PublishInFrequentlyAskedQuestions { get; set; }
    }
}
