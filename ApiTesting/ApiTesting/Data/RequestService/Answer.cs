using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTesting.Data
{
    public class Answer
    {
        public string TextTranslated { get; set; }
        public int RequestId { get; set; }
        public int AnswerId { get; set; }
        public bool IsPreferred { get; set; }

    }
}
