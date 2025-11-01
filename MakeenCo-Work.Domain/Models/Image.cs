using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Domain.Models
{
    public class Image
    {
        public Guid Id { get;private set; }
        public string  ImageUrl { get;private set; }

        public Image()
        {
            
        }

        public Image(string imageUrl)
        {
            ImageUrl = imageUrl;
            Id = Guid.NewGuid();
        }
     
            
      
    }
}
