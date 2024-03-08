using System;
using System.Collections;
using System.Collections.Generic;
namespace IFN664
{

	public class Movie
	{

        public string Title { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int Duration { get; set; }
		public int Quantity { get; set; }
		public int AvailableQuantity { get; set; }
        public List<Member> Renters{ get; set; }
       
        public Movie()
        {
           Renters = new List<Member>();    
        }


    }




}

