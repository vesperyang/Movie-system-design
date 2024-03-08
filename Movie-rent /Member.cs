using System;
namespace IFN664
{
	public class Member
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactPhoneNumber { get; set; }
		public string Password { get; set; }
        public List<Movie> BorrowedMovies { get; set; }
        public Member()
        {
            BorrowedMovies = new List<Movie>();
        }


    }
}

