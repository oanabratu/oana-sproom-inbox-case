using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NullMailService : INullMailService
    {
        private readonly ILogger<NullMailService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message"></param>
        public void SendEmail(string to, string message)
        {
            //Log the message
            _logger.LogInformation(message);
        }
    }
}
