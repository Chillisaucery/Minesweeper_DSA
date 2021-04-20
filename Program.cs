using Minesweeper.classes;
using System;

namespace Minesweeper
{
    /// <summary>
    /// The class that contains the function 'main' to start the program.
    /// It receives required values to setup the game (height, width and mine numbers)
    /// Contributors: Chillisauce 20 April
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to minesweeper.");

            //Input and store Height and width
            Console.WriteLine("Input the height and length of the board: ");

            int height = Convert.ToInt32(Console.ReadLine());
            int width = Convert.ToInt32(Console.ReadLine());

            //Input and store Mine number
            Console.WriteLine("Input the mine number: ");

            int mineNum = Convert.ToInt32(Console.ReadLine());

            //Initialise (create a new empty) board
            Board board = new Board(height, width, mineNum);

            //Fill the board with values
            board.TestBoard();
            
            //Start the game
            board.Start();
        }
    }
}
