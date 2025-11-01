using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeenCo_Work.Domain.Models
{
    public class MoreThanAcademy : Image
    {
     
        public string FirstWord { get; private set; }

        public string SecondWord { get; private set; }

        public string ThirdWord { get; private set; }

        public string FourthWord { get; private set; }

        public DateTime UpdateAt { get; private set; }

        public MoreThanAcademy() { }

        public MoreThanAcademy(string firstWord , string secondWord , string thirdWord , string fourthWord)
        {
            FirstWord = firstWord;
            SecondWord = secondWord;
            ThirdWord = thirdWord;
            FourthWord = fourthWord;
            UpdateAt = DateTime.UtcNow;
           
        }


    }
}
