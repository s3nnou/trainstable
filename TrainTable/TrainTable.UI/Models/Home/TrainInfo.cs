using System;
using TrainTable.DAL.Enums;

namespace TrainTable.UI.Models.Home
{
    public class TrainInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public DateTime DepertureTime { get; set; }

        public DateTime DestinationTime { get; set; }
        public TimeSpan Time { get; set; }

        public TrainType Type { get; set; }
    }
}
