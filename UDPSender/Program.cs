
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UDPSender;

namespace UDPSender
{
    class Program
    {
        private static bool _readFromFile = false;

        public static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += SendPacket;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");

            Console.WriteLine("1. Read from file");
            Console.WriteLine("2. Debug packet");
            Console.WriteLine("X: Close program");

            string? input = string.Empty;

            while (input != "x")
            {
                input = Console.ReadLine();

                switch (input?.ToLower())
                {
                    case "1":
                        _readFromFile = true;
                        break;
                    case "2":
                        _readFromFile = false;
                        break;
                    case "x":
                        timer.Enabled = false;
                        break;
                }
            }
        }

        private static void SendPacket(object source, System.Timers.ElapsedEventArgs e)
        {
            string jsonData = string.Empty;

            if (!_readFromFile)
            {
                NearbyPlayer test = new NearbyPlayer();
                // Populate test data here.
                
                jsonData = JsonConvert.SerializeObject(test);
            }
            else
            {
                // Hacky way but lets you have pretty json in the file
                jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\packet.json");
                var dummy = JsonConvert.DeserializeObject<dynamic>(jsonData);
                jsonData = JsonConvert.SerializeObject(dummy);
            }

            if (!string.IsNullOrEmpty(jsonData))
            {
                using (UdpClient client = new UdpClient(4242))
                {
                    IPAddress address = IPAddress.Parse("224.0.0.5");


                    Console.WriteLine("Sending nearby player packet");

                    byte[] encodedData = Encoding.ASCII.GetBytes(jsonData);

                    Console.WriteLine(jsonData);

                    IPEndPoint endpoint = new IPEndPoint(address, 4242);
                    client.Send(encodedData, encodedData.Length, endpoint);
                }
            }
        }
    }
}


