using System;
using TrainTable.DAL.Enums;

namespace TrainTable.DAL.Models
{
    public class Train
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartureId { get; set; }

        public int DestinationId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime DestinationTime { get; set; }

        public TrainType TypeId { get; set; }
    }
}
