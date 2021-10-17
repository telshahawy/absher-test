using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.Poll
{
    public class PollDto
    {
        public int PollId { get; set; }
        public string PollQuestion { get; set; }
        public int VotersNumber { get; set; }
    }
}
