using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.Answer
{
   public class AnswerDto
    {

        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int VotersNumber { get; set; }

        public string Percent { get; set; }
    }
}
