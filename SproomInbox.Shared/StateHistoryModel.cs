using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Shared
{
    public class StateHistoryModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public StateModel State { get; set; }
        public string? Username { get; set; }
    }
}
