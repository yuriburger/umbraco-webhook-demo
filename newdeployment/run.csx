#r "Newtonsoft.Json"
#r "SendGrid"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json; 
using SendGrid.Helpers.Mail; 
using System.Text;

public async static Task<IActionResult> Run(HttpRequest req, IAsyncCollector<SendGridMessage> messages, TraceWriter log)
{
 log.Info("SendGrid message"); 
 using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8))
 {
    var body = await reader.ReadToEndAsync();
    var subject = String.Empty;

    dynamic data = JsonConvert.DeserializeObject(body);
    subject = data?.ProjectName;

    var message = new SendGridMessage();
    
    message.AddTo("xyz@outlook.com");
    message.AddContent("text/html", body);
    message.SetFrom("xyz@domain.com");
    message.SetSubject(subject);

    await messages.AddAsync(message); 
    return (ActionResult)new OkObjectResult("The E-mail has been sent.");
 }
}