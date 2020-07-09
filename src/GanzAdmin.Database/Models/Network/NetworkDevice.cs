using LiteDB;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace GanzAdmin.Database.Models
{
    public class NetworkDevice : IEntity
    {
        public enum PingResult
        {
            Unknown,
            PingInProgress,
            Found,
            NotFound
        }

        [BsonId]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        [BsonIgnore]
        public string DisplayValue
        {
            get { return this.Name; }
        }

        [BsonIgnore]
        public PingResult Pinged { get; set; } = PingResult.Unknown;

        public void Ping()
        {
            this.Pinged = PingResult.PingInProgress;

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            IPAddress ip = IPAddress.Parse(this.Address);
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            PingReply reply = pingSender.Send(ip, timeout, buffer, options);

            this.Pinged = reply.Status == IPStatus.Success ? PingResult.Found : PingResult.NotFound;
        }
    }
}
