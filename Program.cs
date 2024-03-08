using System;
using System.Collections;

namespace IFN664
{
    class program
    {
        static MemberCollection memberCollection = new MemberCollection(100);
        static string title, classification, genre, FirstName, LastName, ContactPhonenumber;
        static int duration, quantity, Password;
        static void Main(string[] arg)
        {
            MainMenu();
        }
        static void staffLogin ()
        {
            Console.Write("Staff ID:");
            string staffID = Console.ReadLine();
            Console.Write("Password:");
            string staffPassword = Console.ReadLine();
            if (staffID == "staff" && staffPassword == "today123")
            {
                staffMenu();
            }
            else
            {
                Console.WriteLine("staff Id or password is not right.please try again");
                MainMenu();
            }
                
        }
       
       

        static void MainMenu()
        {
            Console.WriteLine("================================================");
            Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine("================================================");
            Console.WriteLine("");
            Console.WriteLine("Main Menu");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Select from the following");
            Console.WriteLine("1.Staff");
            Console.WriteLine("2.Member");
            Console.WriteLine("0.End the program");
            Console.WriteLine("");
            Console.WriteLine("enter your choice==>");
            string enterChoice;
            enterChoice =Console.ReadLine();
            switch (enterChoice)
            {
                case "1":
                    staffLogin();
                    break;
                case "2":
                    Console.WriteLine("Enter first name:");
                    string loginFirstName = Console.ReadLine();

                    Console.WriteLine("Enter last name:");
                    string loginLastName = Console.ReadLine();

                    Console.WriteLine("Enter password:");
                    string loginPassword = Console.ReadLine();

                    if (memberCollection.Login(loginFirstName, loginLastName, loginPassword))
                    {
                        Member member = memberCollection.FindMember(loginFirstName, loginLastName);

                        Console.WriteLine("Login successful!");
                        memberMenu(member);
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Invalid credentials!");
                        MainMenu();
                    }
                    
                    break;
                  
                case "0":
                    Exit();
                    break;
            }

        }

        static void Exit()
        {
            Console.WriteLine("now you have exitted this program");
            MainMenu();

        }

        static void staffMenu()
        {
            int capacity = 1000;
           
            Member member = new Member();
            MyHashtable myHashtable = new MyHashtable();
            Console.WriteLine("Staff Menu");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("1.Add DVDs of a new movie to the system");
            Console.WriteLine("2.Add new DVDs of an existing movie to the system");
            Console.WriteLine("3.Remove DVDs of an existing movie from the system");
            Console.WriteLine("4.Register a new member to the system");
            Console.WriteLine("5.Remove a registered member from the system");
            Console.WriteLine("6.Find a member's contact phone number,given the member's name");
            Console.WriteLine("7.Find members who are currently renting a particular movie");
            Console.WriteLine("0.Return to main menu");
            Console.WriteLine("");
            Console.WriteLine("Enter your choice==>");
            string staffChoice = Console.ReadLine();
            switch (staffChoice)
            {
                case "1":
                    MovieCollection.AddMovie();
                    staffMenu();
                    break;
                case "2":
                    MovieCollection.AddDvds();
                    staffMenu();
                    break;
                case "3":
                    MovieCollection.DeleteDvds();
                    staffMenu();
                    break;
                case "4":
                    memberCollection.RegisterMember();
                    Console.WriteLine("Registration successful!");

                    staffMenu();
                    break;
                case "5":
                    Console.WriteLine("Enter the first name of the member to remove:");
                    string removeFirstName = Console.ReadLine();

                    Console.WriteLine("Enter the last name of the member to remove:");
                    string removeLastName = Console.ReadLine();

                    memberCollection.RemoveMember(removeFirstName, removeLastName);
                   
                   
                    staffMenu();
                    break;
                case "6":
                    Console.WriteLine("Enter the first name of the member to find:");
                    string findFirstName = Console.ReadLine();

                    Console.WriteLine("Enter the last name of the member to find:");
                    string findLastName = Console.ReadLine();

                    Member foundMember = memberCollection.FindMember(findFirstName, findLastName);
                    if (foundMember != null)
                    {
                        Console.WriteLine($"Member found: {foundMember.FirstName} {foundMember.LastName}");
                        Console.WriteLine($"Phone number: {foundMember.ContactPhoneNumber}");
                    }
                    else
                    {
                        Console.WriteLine("Member not found!");
                    }
                    staffMenu();
                    break;
                case "7":
                    Console.WriteLine("Enter the movie title:");
                    string title = Console.ReadLine();
                    MovieCollection.DisplayMembersRentOneMovie(title);
                    staffMenu();
                    break;
                case "0":
                    MainMenu();
                    break;

            }

        }

        static void memberMenu(Member member)
        {
            Console.WriteLine("Member Menu");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("1.Browse all the movies");
            Console.WriteLine("2.Display all the information about a movie");
            Console.WriteLine("3.Borrow a movie DVD");
            Console.WriteLine("4.Return a movie DVD");
            Console.WriteLine("5.List current borrowing movies");
            Console.WriteLine("6.Display the top 3 movies rented by the members");
            Console.WriteLine("0.Return to main menu");
            Console.WriteLine("");
            Console.WriteLine("Enter your choice==>");
            string memberChoice = Console.ReadLine();
            switch (memberChoice)
            {
                case "1":
                    MovieCollection.DisplayMovie();
                    memberMenu(member);
                    break;
                case "2":
                    MovieCollection.DisplayInformation();
                    memberMenu(member);
                    break;
                case "3":
                    Console.WriteLine("enter movie's title u want to borrow:");
                    title = Console.ReadLine();
                    MovieCollection.BorrowMovie(title,member);
                    memberMenu(member);
                    break;
                case "4":
                    Console.WriteLine("enter movie's title u want to return:");
                    title = Console.ReadLine();
                    MovieCollection.RerurnMovie(title, member);
                    memberMenu(member);
                    break;
                case "5":
                    memberCollection.ListBorrowedMovies(member);
                    memberMenu(member);
                    break;
                case "6":
                    memberCollection.DisplayTopThreeFrequentlyBorrowedMovies();
                    memberMenu(member);
                    break;
                case "0":
                    MainMenu();
                    break;
            }

        }
        
    }
}