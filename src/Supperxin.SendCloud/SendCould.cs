using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Supperxin.SendCloud.Utility;

namespace Supperxin.SendCloud
{
    public class SendCould
    {
        private const string IdParaName = "apiUser";
        private const string KeyParaName = "apiKey";
        private const string FromParaName = "from";
        private const string ToParaName = "to";
        private const string SubjectParaName = "subject";
        private const string HtmlParaName = "html";
        private const string XsmtpapiParaName = "xsmtpapi";
        private const string ApiUrl = "http://api.sendcloud.net/apiv2/mail/send";
        private const string AddressSplitor = ";";
        private class SendCloudResult
        {
            public bool result { get; set; }
            public int statusCode { get; set; }
            public string message { get; set; }
        }
        public static TMailMessage NewMail<TMailMessage>()
            where TMailMessage : new()
        {
            return new TMailMessage();
        }

        public static Task<SendResult> SendMail(SendCloudMessage mailMessage, Credential credential)
        {
            if (null == mailMessage || null == credential)
            {
                throw new ArgumentNullException("mailMessage or credential can't be null!");
            }

            var formData = new Dictionary<string, string>();
            formData.Add(IdParaName, credential.Id);
            formData.Add(KeyParaName, credential.Key);

            if( mailMessage.Parameters.Count > 0 )
            {
                formData.Add(XsmtpapiParaName, BuildXsmtpJson(mailMessage.Parameters, mailMessage.To));
            }
            formData.Add(SubjectParaName, mailMessage.Subject);
            formData.Add(HtmlParaName, mailMessage.Html);
            formData.Add(FromParaName, mailMessage.From.ToString());
            formData.Add(ToParaName, BuildAddressList(mailMessage.To));

            try
            {
                var resultJson = HttpService.HttpPost(ApiUrl, formData, Encoding.UTF8);
                var jObject = JObject.Parse(resultJson);
                var result = jObject.ToObject<SendCloudResult>();
                return Task.FromResult<SendResult>(new SendResult()
                {
                    Successful = result.result,
                    ErrorMessage = result.message,
                    Result = result.message
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult<SendResult>(new SendResult()
                {
                    Successful = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        internal static string BuildXsmtpJson(List<Parameter> parameters, List<MailAddress> to)
        {
            JObject obj = new JObject();
            JsonBuilder.BuildWithToString(obj, to, "to");

            var subJson = JObject.Parse("{}");
            foreach (var para in parameters)
            {
                JsonBuilder.BuildDirectly(subJson, para.ValueList, para.Key, "%");
            }

            obj["sub"] = subJson;

            return WebUtility.UrlEncode(obj.ToString());
        }

        private static string BuildAddressList(List<MailAddress> to)
        {
            if (null == to || to.Count == 0)
                return string.Empty;

            if( to.Count == 1)
                return to.First().ToString();

            var addressBuilder = new StringBuilder();
            foreach(var address in to)
            {
                addressBuilder.AppendFormat("{0}{1}", address.ToString(), AddressSplitor);
            }

            return addressBuilder.ToString();
        }

        public static Task<SendResult> SendMail(string subject, string html, string from, string to, Credential credential)
        {
            if (null == credential)
            {
                throw new ArgumentNullException("credential can't be null!");
            }

            var message = new SendCloudMessage(){
                Subject = subject,
                Html = html,
                From = new MailAddress(from),
                To = new List<MailAddress>(){new MailAddress(to)}
            };

            return SendMail(message, credential);
        }
    }
}
