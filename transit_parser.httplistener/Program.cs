using System.Net;
using System.Text.Json;
using transit_parser.Services;

const string address = "localhost";
const int port = 5225;

var transitService = new TransitService();

var listener = new HttpListener();
listener.Prefixes.Add($"http://{address}:{port}/");
listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
listener.Start();

while (true)
{
    Receive();
}

void Receive()
{
    var result = listener.BeginGetContext(ListenerCallback, listener);
    result.AsyncWaitHandle.WaitOne(3000);
}

void ListenerCallback(IAsyncResult result)
{
    if (!listener.IsListening) return;
    
    var context = listener.EndGetContext(result);

    if (context.Request.HttpMethod == "GET" & context.Request.Url?.AbsolutePath == "/Transit/Schedule")
    {
        // Console.WriteLine($"{request.UserHostAddress} => {request.Url}");
        try
        {
            var requestedRoute = context.Request.QueryString.Get("route") ?? string.Empty;
            var responseBody = transitService.AcquireRoute(requestedRoute);
            var responseBuffer = JsonSerializer.SerializeToUtf8Bytes(responseBody);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = responseBuffer.Length;
            context.Response.OutputStream.Write(responseBuffer, 0, responseBuffer.Length);
        }
        catch
        {
            context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        }
    }

    context.Response.Close();
    result.AsyncWaitHandle.Close();
}
