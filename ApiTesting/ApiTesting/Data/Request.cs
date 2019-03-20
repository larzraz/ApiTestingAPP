using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTesting.Data
{
    public class Request
    {
        public Request()
        {
            Answers = new HashSet<Answer>();
        }
        public string LanguageOrigin { get; set; }
        public string LanguageTarget { get; set; }
        public string TextToTranslate { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
