using System.Collections.Generic;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.Templetes.ViewModels
{
    public class NewsLetterViewModel
    {
        public List<NewsLetterDto> newsLetterData { get; set; }

        public NewsLetterViewModel(List<NewsLetterDto> newsLetterData)
        {
            this.newsLetterData = newsLetterData;
        }
    }
}