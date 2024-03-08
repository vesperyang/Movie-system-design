using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq;
namespace IFN664
{
	public class MyHashtable
	{

		private const int Size = 1000;
		private Movie[] movieTable;


		public MyHashtable()
		{
			movieTable = new Movie[Size];
		}
		public void AddNewMovie(string title, string classification, string genre, int duration, int quantity)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] == null)
			{
				Movie movie = new Movie
				{
					Title = title,
					Classification = classification,
					Duration = duration,
					Genre = genre,
					Quantity = quantity,
					AvailableQuantity = quantity,
				};
				if (movie.AvailableQuantity >= 1)
				{
                    movieTable[hashCode] = movie;
                    Console.WriteLine("Movie added successfully.");
                }
				else
				{
                    Console.WriteLine("movie's dvds have to be at lease 1");
                }

				
			}
			else
			{
				Console.WriteLine("Movie already exists in the collection");
			}
		}
		public void AddExistMovie(string title, int quantity)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				movieTable[hashCode].Quantity += quantity;
				Console.WriteLine("movie quantity updated successfully.");
			}
			else
			{
				Console.WriteLine("Movie is not found in the table.");
			}
		}
		public void RemoveDvds(string title, int quantity)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				if (quantity <= movieTable[hashCode].Quantity)
				{
					movieTable[hashCode].Quantity = movieTable[hashCode].Quantity - quantity;
					if (movieTable[hashCode].Quantity!= 0)
					{
                        Console.WriteLine($"movie:{movieTable[hashCode].Title}'s dvds are removed. now quantity is:{movieTable[hashCode].Quantity} ");
                        
                    }
					else
					{
                        Console.WriteLine($"{movieTable[hashCode].Title}doesn't have any dvd, movie is deleted.");
                        movieTable[hashCode].Title = null;
                       
                    }
				}
				else
				{
					Console.WriteLine("the quantity is over than the storage.");
				}
			}
			else
			{
				Console.WriteLine("Movie is not found in the collection");
			}
		}
		public void RemoveMovie(string title)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				movieTable[hashCode] = null;
				Console.WriteLine("Movie not found in the table");
			}
		}

		private int GetHash(string key)
		{
			int hash = 0;
			foreach (char c in key)
			{
				hash += (int)c;
			}
			return hash % Size;
		}
		public void DisplayAllMovies()
		{
			Movie[] sortedMovies = new Movie[movieTable.Length];
			Array.Copy(movieTable, sortedMovies, movieTable.Length);
			Array.Sort(sortedMovies, (m1, m2) =>
			{
				if (m1 == null && m2 == null)
					return 0;
				if (m1 == null)
					return 1;
				if (m2 == null)
					return -1;
				return string.Compare(m1.Title, m2.Title, StringComparison.OrdinalIgnoreCase);
			});

			foreach (Movie movie in sortedMovies)
			{
				if (movie != null)
				{
					Console.WriteLine(movie.Title);
				}
			}
		}
		public void DisplayInformation(string title)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				Console.WriteLine($"Classification: {movieTable[hashCode].Classification}");
				Console.WriteLine($"Genre :{movieTable[hashCode].Genre}");
				Console.WriteLine($"Duration: {movieTable[hashCode].Classification}");
				Console.WriteLine($"Quantity {movieTable[hashCode].Quantity}");
				Console.WriteLine($"Available quantity:{movieTable[hashCode].AvailableQuantity}");

			}
			else
			{
				Console.WriteLine("Movie doesn't exist");
			}
		}
		public void findMovie(string title)
		{
			int hashCode = GetHash(title);
			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				Console.WriteLine($"Movie has been found:{movieTable[hashCode].Title}");

            }
			else
			{
				Console.WriteLine("Movie doesn't exist");
			}


		}
		public void BorrowMovie(string title, Member member)
		{
			int hashCode = GetHash(title);

			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				Movie movie = movieTable[hashCode];

				if (movie.AvailableQuantity > 0)
				{
					movie.AvailableQuantity--;
					member.BorrowedMovies.Add(movie);
					movie.Renters.Add(member);
					Console.WriteLine("DVD borrowed successfully.");
				}
				else
				{
					Console.WriteLine("All DVDs of this movie are currently borrowed.");
				}
			}
			else
			{
				Console.WriteLine("Movie not found in the table.");
			}
		}
		public void DisplayMembersRentMovie(string title)
		{
			int hashCode = GetHash(title);

			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				Movie movie = movieTable[hashCode];

				foreach (Member member in movie.Renters)
				{
					
                        Console.WriteLine($"- {member.FirstName + member.LastName} are renting this movie");
        			
				}

			}
			else
			{
				Console.WriteLine("Movie is not found");
			}
			
		}

		public void ReturnMovie(string title, Member member)
		{
			int hashCode = GetHash(title);

			if (movieTable[hashCode] != null && movieTable[hashCode].Title == title)
			{
				Movie movie = movieTable[hashCode];

				if (movie.AvailableQuantity < movie.Quantity)
				{
					movie.AvailableQuantity++;
					member.BorrowedMovies.Remove(movie);
					movie.Renters.Remove(member);
					Console.WriteLine("DVD returned successfully.");
				}
				else
				{
					Console.WriteLine("No DVDs of this movie are currently borrowed.");
				}
			}
			else
			{
				Console.WriteLine("Movie not found in the table.");
			}
		}



	}

	public class MovieCollection
	{
		static MyHashtable movieHashtable = new MyHashtable();

		public static void AddMovie()
		{

			Console.Write("Enter movie title:");
			string title = Console.ReadLine();
			Console.Write("Enter movie's classification(G,PG,M15,MA15,):");
			string classification = Console.ReadLine();
			Console.Write("Enter movie Genre(Drama,Adventure,Family,Action,SciFi,Comedy,Animated,Thriller,Other,):");
			string genre = Console.ReadLine();
			Console.Write("Enter movie's duration:");
			int duration = Convert.ToInt32(Console.ReadLine());
			Console.Write("Enter dvd's quantity:");
			int quantity = Convert.ToInt32(Console.ReadLine());
			movieHashtable.AddNewMovie(title, classification, genre, duration, quantity);

		}
		public static void DeleteMovie()
		{

			Console.Write("Enter movie title to delete:");
			string title = Console.ReadLine();
			movieHashtable.RemoveMovie(title);

		}
		public static void AddDvds()
		{

			Console.Write("Enter movie title to add Dvds:");
			string title = Console.ReadLine();
			Console.Write("Enter the quantity u want to add:");
			int quantity = Convert.ToInt32(Console.ReadLine());
			movieHashtable.AddExistMovie(title, quantity);


		}
		public static void DeleteDvds()
		{

			Console.Write("Enter movie title to remove Dvds:");
			string title = Console.ReadLine();
			Console.Write("Enter the quantity u want to remove:");
			int quantity = Convert.ToInt32(Console.ReadLine());
			movieHashtable.RemoveDvds(title, quantity);
			
		}
		public static void DisplayMovie()
		{
			Console.WriteLine("List of all movies:");
			movieHashtable.DisplayAllMovies();
		}
		public static void DisplayInformation()
		{
			Console.WriteLine("enter movie's title");
			string title = Console.ReadLine();
			movieHashtable.DisplayInformation(title);

		}

		public static void BorrowMovie(string title, Member member)
		{
			movieHashtable.BorrowMovie(title, member);
		}
		public static void RerurnMovie(string title, Member member)
		{
			movieHashtable.ReturnMovie(title, member);
		}
        public static void DisplayMembersRentOneMovie(string title)
        {
			movieHashtable.DisplayMembersRentMovie(title);
        }


    }
}
	


