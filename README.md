# 功能

SendCloud发送Mail的 .NET Core 版本，可以在 Asp.Net Core中利用SendCould来发送Mail

# 安装

    Install-Package Supperxin.SendCloud

package information:

    https://www.nuget.org/packages/Supperxin.SendCloud

# 使用

添加命名空间：

```c#
using Supperxin.SendCloud;
```

提供两个方法发送Mail：

方法1：

```c#
public static Task<SendResult> SendMail(string subject, string html, string from, string to, Credential credential)
```

用法：

```c#
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
//Assert.True(result.Successful);
```

方法2：

```c#
public static Task<SendResult> SendMail(SendCloudMessage mailMessage, Credential credential)
```

用法：

```c#
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
//Assert.True(result.Successful);
```

