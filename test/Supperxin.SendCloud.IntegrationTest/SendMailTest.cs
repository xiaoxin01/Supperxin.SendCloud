using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Supperxin.SendCloud.IntegrationTest
{
    public class SendMailTest
    {
        [Fact]
        public void TestSendMailDirectly()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("Configuration.json")
                .Build();

            var credential = new Credential()
            {
                Id = config["SendCloudId"],
                Key = config["SendCloudKey"]
            };

            var subject = "Test Send Mail";
            var html = "This is a test mail";
            var from = config["MailFrom"];
            var to = config["MailTo"];

            var result = SendCould.SendMail(subject, html, from, to, credential).Result;
            Assert.True(result.Successful);
        }


        [Fact]
        public void TestSendMailWithMessage()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("Configuration.json")
                .Build();

            var credential = new Credential()
            {
                Id = config["SendCloudId"],
                Key = config["SendCloudKey"]
            };

            var subject = "Test Send Mail With Message";
            var html = "This is a test mail with message";
            var from = config["MailFrom"];
            var to = config["MailTo"];

            var message = new SendCloudMessage(){
                Subject = subject,
                Html = html,
                From = new MailAddress(from),
                To = new List<MailAddress>(){new MailAddress(to)}
            };

            var result = SendCould.SendMail(message, credential).Result;
            Assert.True(result.Successful);
        }
    }
}
