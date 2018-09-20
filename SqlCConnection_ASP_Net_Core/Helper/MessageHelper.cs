using Chayns.Backend.Api.Credentials;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Repository;
using Microsoft.Extensions.Options;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Helper
{
    public class MessageHelper : IMessageHelper
    {
        public ChaynsApiInfo _backendApiSettings;

        public MessageHelper(IOptions<ChaynsApiInfo> backendApiSettings)
        {
            _backendApiSettings = backendApiSettings.Value;
        }
        public bool SendIntercom(string message)
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430010);
            var intercomRepository = new IntercomRepository(secret);
            var intercomData = new IntercomData(158753)
            {
                Message = message,
                UserIds = new List<int>
                {
                    1808455
                },
            };
            var result = intercomRepository.SendIntercomMessage(intercomData);
            return result.Status.Success;
        }
    }
}