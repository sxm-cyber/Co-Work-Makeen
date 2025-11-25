using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class FAQ : BaseModel
	{
		[Required , MaxLength(500)]
		public string Question { get; private set; }

		[Required]
		public string Answer { get; private set; }

		public bool IsActive { get;private  set; }

        public bool PublishInMainPage { get;private set; }

        public bool PublishInFrequentlyAskedQuestions { get;private set; }
		
		private FAQ() { }


		public FAQ(string question,string answer,bool publishInFrequentlyAskedQuestions,bool publishInMainPage,bool isActive)
		{
			Question = question;
			Answer = answer;
			PublishInFrequentlyAskedQuestions = publishInFrequentlyAskedQuestions;
			PublishInMainPage = publishInMainPage;
			IsActive = isActive;
		}

		public void Update(string question , string answer , bool publishInMainPage, bool publishInFrequentlyAskedQuestions, bool isActive)
		{
			Question = question;
			Answer = answer;
			PublishInMainPage = false;
			PublishInFrequentlyAskedQuestions = false;
			IsActive = isActive;
			UpdateTimestamp();
		}
	}
}

