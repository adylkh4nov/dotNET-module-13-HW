using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_module_13_HW
{

    public class Person
    {
        public int ID { get; }
        public string Type { get; }

        public Person(int id, string type)
        {
            ID = id;
            Type = type;
        }
    }

    public class BankSystem : IEnumerable<Person>
    {
        private Queue<Person> queue = new Queue<Person>();

        public void EnterQueue(Person person)
        {
            queue.Enqueue(person);
            Console.WriteLine($"Person with ID {person.ID} joined the queue for {person.Type}.");
            DisplayQueue();
        }

        public Person ExitQueue()
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("Queue is empty.");
                return null;
            }

            Person person = queue.Dequeue();
            Console.WriteLine($"Person with ID {person.ID} for {person.Type} was served.");
            DisplayQueue();
            return person;
        }

        public void DisplayQueue()
        {
            Console.WriteLine("Current Queue:");
            foreach (Person person in queue)
            {
                Console.WriteLine($"Person ID: {person.ID}, Type: {person.Type}");
            }
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BankOperator
    {
        private BankSystem bankSystem = new BankSystem();
        private int personID = 1;

        public void AddPerson(string type)
        {
            Person person = new Person(personID++, type);
            bankSystem.EnterQueue(person);
        }

        public void ServeNextPerson()
        {
            bankSystem.ExitQueue();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BankOperator bankOperator = new BankOperator();

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Join the queue.");
                Console.WriteLine("2. Serve next person.");
                Console.WriteLine("3. Exit.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter person type (e.g., deposit, withdrawal, consultation):");
                        string type = Console.ReadLine();
                        bankOperator.AddPerson(type);
                        break;
                    case "2":
                        bankOperator.ServeNextPerson();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

}
