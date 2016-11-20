using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Media.Protection.PlayReady;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace PifaceLedControl
{
    public sealed class PiWebServer

    {
        public PiWebServer()
        {

        }

        public void Initialise()
        {
            listener = new StreamSocketListener();

            listener.BindServiceNameAsync("1111");

            listener.ConnectionReceived += async (sender, args) =>
            {
                // call the handle request function when a request comes in
                HandleRequest(sender, args);
            };

        }

        public async void HandleRequest(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            StringBuilder request = new StringBuilder();

            // Handle a incoming request
            // First read the request
            using (IInputStream input = args.Socket.InputStream)
            {
                byte[] data = new byte[BufferSize];
                IBuffer buffer = data.AsBuffer();
                uint dataRead = BufferSize;
                while (dataRead == BufferSize)
                {
                    await input.ReadAsync(buffer, BufferSize, InputStreamOptions.Partial);
                    request.Append(Encoding.UTF8.GetString(data, 0, data.Length));
                    dataRead = buffer.Length;
                }
            }

            var query = GetQuery(request);
            string resultText = OnOffLed(query);

            // Send a response back
            using (IOutputStream output = args.Socket.OutputStream)
            {
                using (Stream response = output.AsStreamForWrite())
                {
                    // For now we are just going to reply to anything with Hello World!
                    byte[] bodyArray = Encoding.UTF8.GetBytes(string.Format("<html><body>{0}</body></html>", resultText));

                    var bodyStream = new MemoryStream(bodyArray);

                    // This is a standard HTTP header so the client browser knows the bytes returned are a valid http response
                    var header = "HTTP/1.1 200 OK\r\n" +
                                $"Content-Length: {bodyStream.Length}\r\n" +
                                    "Connection: close\r\n\r\n";

                    byte[] headerArray = Encoding.UTF8.GetBytes(header);

                    // send the header with the body inclded to the client
                    await response.WriteAsync(headerArray, 0, headerArray.Length);
                    await bodyStream.CopyToAsync(response);
                    await response.FlushAsync();
                }
            }
        }

        private string OnOffLed(string[] query)
        {
            StringBuilder resultText = new StringBuilder();
            int targetLedNo;
            if (int.TryParse(query[0], out targetLedNo) == false && targetLedNo > 7)
                return "XXXXXXX";
            else
                resultText.AppendFormat("{0}번 LED", targetLedNo);

            MCP23S17.WritePin(PFDII.LedAdress[targetLedNo], this.CheckLedStatus(PFDII.LedAdress[targetLedNo]) == MCP23S17.On ? MCP23S17.Off : MCP23S17.On);

            return resultText.ToString();
        }

        private string[] GetQuery(StringBuilder request)
        {
            var requestLines = request.ToString().Split(' ');

            var url = requestLines.Length > 1
                              ? requestLines[1] : string.Empty;

            var uri = new Uri("http://localhost" + url);
            var query = uri.AbsolutePath.Replace("/", "").Split('|');
            return query;
        }

        private byte CheckLedStatus(byte led)
        {
            UInt16 Inputs = MCP23S17.ReadRegister16();

            return ((Inputs & 1 << led) != 0) ? MCP23S17.On : MCP23S17.Off;
        }

        private StreamSocketListener listener; // the socket listner to listen for TCP requests
                                               // Note: this has to stay in scope!

        private const uint BufferSize = 8192; // this is the max size of the buffer in bytes 
    }
}