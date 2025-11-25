namespace MakeenCo_Work.Domain.Models
{
    public class WaysOfCommunication : BaseModel
    {
        public string Address { get; private set; }

        public string PhoneNumber { get; private set; }

        public string LandlineNumber { get; private set; }

        public string BaleLink { get; private set; }

        public string InstagramLink { get; private set; }

        public string LinkdinLink { get; private set; }

        public string MakeenWebsiteLink { get; private set; }


        public WaysOfCommunication() { }


        public WaysOfCommunication(string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink)
        {
            Address = address;
            PhoneNumber = phoneNumber;
            LandlineNumber = landlineNumber;
            BaleLink = baleLink;
            InstagramLink = instagramLink;
            LinkdinLink = linkdinLink;
            MakeenWebsiteLink = makeenWebsiteLink;
        }


        public void Update(string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink)
        {
            Address = address;
            PhoneNumber = phoneNumber;
            LandlineNumber = landlineNumber;
            BaleLink = baleLink;
            InstagramLink = instagramLink;
            LinkdinLink = linkdinLink;
            MakeenWebsiteLink = makeenWebsiteLink;
            UpdateTimestamp();
        }
    }
}

