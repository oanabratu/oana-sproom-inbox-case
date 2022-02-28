using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Shared
{
    public class ApproveAllDocumentsParams
    {
        public IList<Guid> DocumentIds { get; set; }

        public string Username { get; set; }
    }
}
