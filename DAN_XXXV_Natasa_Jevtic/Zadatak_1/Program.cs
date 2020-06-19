using System;
using System.Threading;

namespace Zadatak_1
{
    /// <summary>
    /// This program simulates guessing a number.
    /// </summary>
    class Program
    {
        static int numberToGuess;
        static int numberOfParticipants;
        static Thread[] threads;
        static Thread generator;
        /// <summary>
        /// This method sets number of participants and guessing number based on user input, and then starts thread which perform generating another threads.
        /// </summary>
        static void GetInputFromConsole()
        {
            Console.WriteLine("Enter the numbers of players who will try to guess the number (maximum is 1000):");
            string inputForParticipants = Console.ReadLine();
            bool conversion1 = Int32.TryParse(inputForParticipants, out numberOfParticipants);
            while (!conversion1 || numberOfParticipants <= 0 || numberOfParticipants > 1000)
            {
                Console.WriteLine("Invalid input. Try again:");
                inputForParticipants = Console.ReadLine();
                conversion1 = Int32.TryParse(inputForParticipants, out numberOfParticipants);
            }
            Console.WriteLine("Enter a number from 1 to 100 to be guessed:");
            string inputForNumber = Console.ReadLine();
            bool conversion2 = Int32.TryParse(inputForNumber, out numberToGuess);
            while (!conversion2 || numberToGuess < 1 || numberToGuess > 100)
            {
                Console.WriteLine("Invalid input. Try again:");
                inputForNumber = Console.ReadLine();
                conversion2 = Int32.TryParse(inputForNumber, out numberToGuess);
            }
            //creating instance of thread which performs method for generating another threads
            generator = new Thread(CreatingParticipants);
            generator.Name = "Thread_Generator";
            generator.Start();
            Console.WriteLine("Number of users and guessed number is selected. Number of players is {0}.", numberOfParticipants);
        }
        /// <summary>
        /// This method creating threads which number is equal to number of participants
        /// </summary>
        static void CreatingParticipants()
        {
            //creating array of threads which length is equal to number of participants
            threads = new Thread[numberOfParticipants];
            //creating threads, setting their names, and then filling array of threads
            for (int i = 0; i < threads.Length; i++)
            {
                Thread t = new Thread(GuessNumber)
                {
                    Name = string.Format("User_{0}", i + 1)
                };
                threads[i] = t;
            }
        }
        static void GuessNumber()
        {

        }
        static void Main(string[] args)
        {
            Thread askForInput = new Thread(GetInputFromConsole);
            askForInput.Start();
            askForInput.Join();
            generator.Join();
            //only when threads is created, starts them 
            if (threads !=null)
            {
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Start();
                }
            }            
            Console.ReadLine();
        }
    }
}
