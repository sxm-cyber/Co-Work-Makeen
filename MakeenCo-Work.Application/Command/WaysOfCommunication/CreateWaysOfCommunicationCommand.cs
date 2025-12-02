using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.Command
{
    public class CreateWaysOfCommunicationCommand
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string LandlineNumber { get; set; }

        public string BaleLink { get; set; }

        public string InstagramLink { get; set; }

        public string LinkdinLink { get; set; }

        public string MakeenWebsiteLink { get; set; }
    }
}
