using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.classes
{
    /// <summary>
    /// State Board contains the state of each cell of the board (revealed or not revealed)
    /// Contributors: Chillisauce 20 April
    /// </summary>
    public class StateBoard
    {
        //Variables
        private int height;
        private int width;

        //The 2D array to contain the state of each cell of the board: that being revealed or not
        //True means revealed, false means not revealed.
        private bool[,] stateBoard;

        //Getter
        public bool[,] getStateBoard()
        {
            return (stateBoard);
        }

        //Constructor. It create a new empty 2D array stateBoard
        public StateBoard(int height, int width)
        {
            //Record the height and width
            this.height = height;
            this.width = width;

            //Create the board
            stateBoard = new bool[height, width];
        }

        //Fill every cells with the value "false"
        private void fillFalse()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    stateBoard[i, j] = false;
        }

        //Fill the target cell with the value "true"
        /// <param name="x"> The coordinate x of the input </param>
        /// <param name="y"> The coordinate y of the input </param>
        public void revealCell(int x, int y)
        {
            stateBoard[x, y] = true;
        }

        //Fill the target cell with the value "true" and expand to the surrounding area, base on the value of indexBoard
        /// <param name="x"> The coordinate x of the input </param>
        /// <param name="y"> The coordinate y of the input </param>
        /// <param name="indexBoard"> The given indexBoard, the values of indexBoard is needed for the expansion to perform correctly</param>
        // The function currently is in development
        public void revealCell(int x, int y, char[,] indexBoard)
        {
            stateBoard[x, y] = true;

            int counter = 0;

            /*
            while (counter <= (height + width) / 4)
            {
                counter++;

                for (int i = x - counter; i <= x + counter; i++)
                    for (int j = y - counter; j <= y + counter; j++)
                        if ((i < height) && (j < width) && (i >= 0) && (j >= 0))
                            if (indexBoard[i, j] != 'X')
                                stateBoard[i, j] = true;
            }
            */
        }

        //Display the stateBoard
        public void displayStateBoard()
        {
            Console.WriteLine("The state board is: ");

            Console.Write("  ");
            for (int j = 0; j < width; j++)
                Console.Write(j);
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < width; j++)
                {
                    if (stateBoard[i, j] == false)
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        //Fill the values into the board
        public void createStateBoard()
        {
            fillFalse();
            displayStateBoard();
        }
    }
}
