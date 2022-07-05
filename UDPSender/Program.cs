
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
        public static void Main(string[] args)
        {

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += SendPacket;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();
        }

        private static void SendPacket(object source, System.Timers.ElapsedEventArgs e)
        {
            NearbyPlayer test = new NearbyPlayer()
            {
                version = "2",
                build = "100672",
                account = "<AccountID>",
                secondary_account = "<AccountID>",
                avatar = 166633186212728153,
                level = 1953,
                pframe = 166633186212712223,
                platform = 1,
                elevel = 2,
                endors = new NearbyPlayer.Endorsement[]
                {
                    new NearbyPlayer.Endorsement()
                    {
                        id = NearbyPlayer.Shotcaller,
                        count = 63
                    },
                    new NearbyPlayer.Endorsement()
                    {
                        id = NearbyPlayer.GoodTeammate,
                        count = 1266
                    },
                    new NearbyPlayer.Endorsement()
                    {
                        id = NearbyPlayer.Sportsmanship,
                        count = 622
                    }
                }
            };

            using (UdpClient client = new UdpClient(4242))
            {
                IPAddress address = IPAddress.Parse("224.0.0.5");

                string jsonData = JsonConvert.SerializeObject(test);

                Console.WriteLine("Sending nearby player packet");

                byte[] encodedData = Encoding.ASCII.GetBytes(jsonData);

                Console.WriteLine(jsonData);

                IPEndPoint endpoint = new IPEndPoint(address, 4242);
                client.Send(encodedData, encodedData.Length, endpoint);
            }            
        }
    }
}


