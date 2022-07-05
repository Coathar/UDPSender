using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPSender
{
    internal class NearbyPlayer
    {
        public string version { get; set; }

        public string build { get; set; }

        public string account { get; set; }

        public string secondary_account { get; set; }

        public long avatar { get; set; }

        public int level { get; set; }

        public long pframe { get; set; }

        public int platform { get; set; }

        public int elevel { get; set; }

        public Endorsement[] endors { get; set; }

        public class Endorsement
        {
            public long id { get; set; }

            public int count { get; set; }
        }

        public static readonly long Shotcaller = 972777519512041796;
        public static readonly long GoodTeammate = 972777519512041797;
        public static readonly long Sportsmanship = 972777519512041798;
    }
}
