namespace MakeenCo_Work.Domain.Models
{
    public class MainBaner : Image
    {
       
        public string FirstSentens { get;private set; }

        public string SecondSentens { get;private set; }


        public MainBaner() { }

        
        public MainBaner(string firstSentens,string secondSentens)
        {
            FirstSentens = firstSentens;
            SecondSentens = secondSentens;
        }
    }
}
