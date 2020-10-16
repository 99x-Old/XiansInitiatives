using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XiansInitiative.Configuration.Accessor;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Constants;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.Service
{
    public class AzureBusService : IAzureBusService
    {
        private const string QueueName = "notificationqueue";

        private readonly IConfigurationAccessor _configurationAccessor;

        public AzureBusService(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public async Task SendEmailAsync(string memberId, string messege)
        {
            var outgoingEmailDto = new OutgoingEmailDto();
            outgoingEmailDto.To = EmailText.To;
            outgoingEmailDto.From = EmailText.From;

            switch (messege)
            {
                case "roleChange":
                    outgoingEmailDto.Subject = EmailText.RoleChangeSubject;
                    outgoingEmailDto.Body = EmailText.RoleChangeBody;
                    break;

                case "initiativeAssign":
                    outgoingEmailDto.Subject = EmailText.InitiativeAssignSubject;
                    outgoingEmailDto.Body = EmailText.InitiativeAssignBody;
                    break;

                case "actionAssign":
                    outgoingEmailDto.Subject = EmailText.ActionAssignSubject;
                    outgoingEmailDto.Body = EmailText.ActionAssignAssignBody;
                    break;

                case "stateChange":
                    outgoingEmailDto.Subject = EmailText.StateChangeSubject;
                    outgoingEmailDto.Body = EmailText.StateChangeBody;
                    break;
            }
            await SendDataToServiceBussAsync(outgoingEmailDto);
        }

        public async Task SendNewsLetterAsync(string templete)
        {
            var outgoingEmailDto = new OutgoingEmailDto();
            outgoingEmailDto.To = EmailText.To;
            outgoingEmailDto.From = EmailText.From;

            outgoingEmailDto.Subject = "Monthly News Letter";
            outgoingEmailDto.Body = templete;
            await SendDataToServiceBussAsync(outgoingEmailDto);
        }

        public async Task SendMeetingInvitationsAsync(string channelId)
        {
            var outgoingEmailDto = new OutgoingEmailDto();
            outgoingEmailDto.To = EmailText.To;
            outgoingEmailDto.From = EmailText.From;

            outgoingEmailDto.Subject = EmailText.InvitationSubject;
            outgoingEmailDto.Body = ("You have invited for a meeting. Link: http://localhost:4200/meetings?channelId=" + channelId);

            await SendDataToServiceBussAsync(outgoingEmailDto);
        }

        public async Task SendDataToServiceBussAsync(OutgoingEmailDto outgoingEmailDto)
        {
            var ServiceBusConnectionString = _configurationAccessor.GetConnectionString("ServiceBusConnectionString");
            IQueueClient queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            try
            {
                string messageBody = JsonSerializer.Serialize(outgoingEmailDto);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                Console.WriteLine($"Sending message: {message}");

                await queueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}