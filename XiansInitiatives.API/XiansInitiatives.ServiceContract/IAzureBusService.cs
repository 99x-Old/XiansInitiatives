using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.ServiceContract
{
    public interface IAzureBusService
    {
        Task SendEmailAsync(string memberId, string messege);

        Task SendMeetingInvitationsAsync(string channelId);

        Task SendNewsLetterAsync(string templete);
    }
}