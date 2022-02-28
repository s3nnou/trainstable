using System;
using TrainTable.DAL.Enums;

namespace TrainTable.DAL.Models
{
    public class Train
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Departure { get; set; }

        public int Destination { get; set; }

        public DateTime DepertureTime { get; set; }

        public DateTime DestinationTime { get; set; }

        public TrainType Type { get; set; }
    }
}
