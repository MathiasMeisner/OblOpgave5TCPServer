using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Obl_Opgave1;
using Newtonsoft.Json;

namespace OblOpgave5TCPServer
{
    class Server
    {
        public void Start()
        {
            while (true)
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");

                TcpListener serverSocket = new TcpListener(ip, port: 4646);
                serverSocket.Start();
                Console.WriteLine("Server started");

                TcpClient connectionSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine("Server activated");

                Stream ns = connectionSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                string message = sr.ReadLine();
                string answer = "";

                Console.WriteLine("Client: " + message);

                if (message.Equals("Getall"))
                {
                    sw.WriteLine("GetAll modtaget");
                    sw.WriteLine(JsonConvert.SerializeObject(Data).ToString());
                }
                else if (message.Equals("Getbyid"))
                {
                    sw.WriteLine("Getbyid modtaget");
                    sw.WriteLine("Skriv id nummer");
                    string messageid = sr.ReadLine();
                    int number = Int32.Parse(messageid);
                    sw.WriteLine(JsonConvert.SerializeObject(Data.Find(Data => Data.Id == number)));
                }

                else if (message.Equals("Save"))
                {
                    sw.WriteLine("Save modtaget");
                    sw.WriteLine("Skriv dit objekt");
                    string beer = sr.ReadLine();
                    Data.Add(JsonConvert.DeserializeObject<Beer>(beer));
                }

                //answer = message.ToUpper();
                //sw.WriteLine(answer);

                else
                {
                    Console.WriteLine("Input not recognized");

                }

                ns.Close();
                Console.WriteLine("This is stopped.");
                connectionSocket.Close();

                serverSocket.Stop();

                //message = sr.ReadLine();

            }

        }

        private static readonly List<Beer> Data = new List<Beer>()
        {
            new Beer(){Name = "Grøn Tuborg", Id = 1, Price = 20, Abv = 4.6 },
            new Beer(){Name = "Tuborg Guld", Id = 2, Price = 25, Abv = 5.6 },
            new Beer(){Name = "Carlsberg Elephant", Id = 3, Price = 29, Abv = 7.2 },

        };

    }
}
