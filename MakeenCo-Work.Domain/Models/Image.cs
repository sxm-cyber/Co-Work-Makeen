namespace MakeenCo_Work.Domain.Models
{
    public class Image : BaseModel
    {
        public string  ImageUrl { get; private set; }


        public Image() { }


        public Image(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
