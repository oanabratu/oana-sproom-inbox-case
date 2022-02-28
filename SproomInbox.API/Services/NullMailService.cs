using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.API.Services
{
    public class NullMailService : INullMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendEmail(string message)
        {
            //Log the messaje
            _logger.LogInformation(message);
        }
    }
}
