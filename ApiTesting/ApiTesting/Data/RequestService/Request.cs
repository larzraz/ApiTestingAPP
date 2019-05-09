using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApiTesting.Data
{
    public class Request
    {
        public Request()
        {

        }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        public bool IsClosed { get; set; }
        public int noOfAnswers { get; set; }

        public int RequestId { get; set; }
    }
}
