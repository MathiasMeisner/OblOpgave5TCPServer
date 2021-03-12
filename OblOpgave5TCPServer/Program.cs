using System;

namespace OblOpgave5TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            server.Start();
        }
    }
}
