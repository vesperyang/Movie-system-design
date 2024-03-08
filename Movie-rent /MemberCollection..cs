using System;
using System.Collections;
using System.Diagnostics.Metrics;

namespace IFN664
{
    public class MemberCollection
    {

        private Member[] members;
        private int count;
        MovieCollection movieCollection = new MovieCollection();
        Movie movie = new Movie();

        public MemberCollection(int capacity)
        {
            members = new Member[capacity];
            count = 0;


        }
        public void AddMember(string firstName, string lastName, string phoneNumber, string password)
        {
            Member member = new Member
            {
                FirstName = firstName,
                LastName = lastName,
                ContactPhoneNumber = phoneNumber,
                Password = password,
            };

            members[count] = member;
            count++;
        }
        public bool Login(string firstName, string lastName, string password)
        {
            Member member = FindMember(firstName, lastName);

            return member != null && member.Password == password;
        }
        public void RegisterMember()
        {
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter phone number:");
            string phoneNumber = Console.ReadLine();

            string password = GeneratePassword();
            
                    Member newMember = new Member
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        ContactPhoneNumber = phoneNumber,
                        Password = password
                    };
           
                members[count] = newMember;
                count++;
                Console.WriteLine($"password is:{newMember.Password} don't tell anyone!");
                 
        }
        public void RemoveMember(string firstName, string lastName)
        {
            Member member = FindMember(firstName, lastName);
            if (member != null)
            {
                int index = Array.IndexOf(members, member);
                if (index >= 0)
                {
                    Array.Copy(members, index + 1, members, index, count - index - 1);
                    count--;
                    Console.WriteLine("Member removed successfully!");
                }
            }
            else
            {
                Console.WriteLine("Movie doesn't exist");
            }
        }
        public Member FindMember(string firstName, string lastName)
        {
            Member member = Array.Find(members, m =>m?.FirstName == firstName && m?.LastName == lastName);
            return member;
        }
        private string GeneratePassword()
        {
            Random random = new Random();
            int password = random.Next(1000, 10000);
            return password.ToString();
        }


        public void ListBorrowedMovies(Member member)
        {
            Console.WriteLine("Borrowed Movies:");
            foreach (Movie movie in member.BorrowedMovies)
            {
                Console.WriteLine($"- {movie.Title}");
            }

        }

        public void DisplayTopThreeFrequentlyBorrowedMovies()
        {

            Dictionary<string, int> BorrowCounts = new Dictionary<string, int>();
            foreach (Member member in members)
            {
                if (member.BorrowedMovies.Count != 0)
                {
                    foreach (Movie movie in member.BorrowedMovies)
                    {
                        if (BorrowCounts.ContainsKey(movie.Title))
                        {
                            BorrowCounts[movie.Title]++;
                        }
                        else
                        {
                            BorrowCounts[movie.Title] = 1;
                        }
                    }

                    var topThreeMovies = BorrowCounts.OrderByDescending(kvp => kvp.Value).Take(3);
                    Console.WriteLine("Top 3 Most Frequently Borrowed Movies:");
                    foreach (var kvp in topThreeMovies)
                    {
                        Console.WriteLine($"Movie: {kvp.Key}, Frequency: {kvp.Value}");
                    }
                }
                if (member.BorrowedMovies.Count == 0)
                {

                    Console.WriteLine("none of the movies has been rented");

                }
            }
        }


    }
}




