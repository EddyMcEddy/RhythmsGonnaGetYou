using System;
using System.Collections.Generic;

namespace RhythmsGonnaGetYou
{
    public class Albums
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public int YearReleased { get; set; }



        public int BandId { get; set; }
        public Bands Band { get; set; }

        public List<Songs> Song { get; set; }

    }
}