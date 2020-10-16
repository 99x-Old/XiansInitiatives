using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace XiansInitiatives.MailFunction
{
    public static class MailFunction
    {
        [FunctionName("MailFunction")]
        public static async void RunAsync(
            [ServiceBusTrigger("notificationqueue", Connection = "serviceBusTrigger")] Message message, ILogger log,
            [SendGrid(ApiKey = "sendGridKeyAppSettingName")] IAsyncCollector<SendGridMessage> messageCollector,
            [CosmosDB(
                databaseName: "XiansNoSQL",
                collectionName: "LogContainer",
                ConnectionStringSetting = "cosmosDBConnection")] IAsyncCollector<object> products,
            ILogger dbLogger)

        {
            OutgoingEmail messegeBody = JsonConvert.DeserializeObject<OutgoingEmail>(Encoding.UTF8.GetString(message.Body));
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {messegeBody.Subject}");

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.AddTo(messegeBody.To);
            sendGridMessage.AddContent("text/html", messegeBody.Body);
            sendGridMessage.SetFrom(new EmailAddress(messegeBody.From));
            sendGridMessage.SetSubject(messegeBody.Subject);

            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function sending email: {messegeBody.Subject}");
                await messageCollector.AddAsync(sendGridMessage);
                log.LogInformation($"C# ServiceBus queue trigger function email sent to : {messegeBody.To}");

                dbLogger.LogError($"Inserting data to cosmosDB...");
                await products.AddAsync(messegeBody);
                dbLogger.LogError($"Insertion successful!");
            }
            catch (Exception ex)
            {
                dbLogger.LogError($"Exception thrown: {ex.Message}");
            }
        }
    }

    public class OutgoingEmail
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}