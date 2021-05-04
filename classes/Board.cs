using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.classes
{
    /// <summary>
    /// The board is the whole gameplay. It receives input of the coordination of x and y, processing the logic and then display the output into the screen.
    /// It contains and link together indexBoard, stateBoard and UIBoard.
    /// Contributors: Chillisauce 20 April
    /// </summary>
    public class Board
    {
        //Components
        private IndexBoard indexBoard;
        private StateBoard stateBoard;
        //private UIBoard uiBoard;

        //Variables
        private int height;
        private int width;
        private int mineNum;

        /// <summary>
        /// Constructor
        /// With given params, it initialise (creates new blank) indexBoard and stateBoard
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="mineNum"></param>
        public Board(int height, int width, int mineNum)
        {
            indexBoard = new IndexBoard(height, width, mineNum);
            stateBoard = new StateBoard(height, width);

            //Record
            this.height = height;
            this.width = width;
            this.mineNum = mineNum;
        }

        // Initialise the boards, fill the indexBoard and the stateBoard with values
        public void TestBoard()
        {
            this.indexBoard.createIndexBoard();
            this.stateBoard.createStateBoard();
        }

        // Start the game, allow the player to input x and y to reveal cells
        public void Start()
        {
            this.indexBoard.createIndexBoard();
            this.stateBoard.createStateBoard();
            Console.Clear();

            bool won = true;
            while (won)
            {
                Console.WriteLine("Input the position to reveal (x,y): ");
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());

                stateBoard.revealCell(x, y, indexBoard.getIndexBoard());
                displayBoard();
                won = this.stateBoard.winCondition(mineNum);
            }
        }
        

        // Display the board to the console
        public void displayBoard()
        {
            Console.WriteLine("The board is: ");

            Console.Write("  ");
            for (int j = 0; j < width; j++)
                Console.Write("\t" + j);
            Console.WriteLine();

            char[,] index = indexBoard.getIndexBoard();
            bool[,] state = stateBoard.getStateBoard();

            for (int i = 0; i < height; i++)
            {
                Console.Write(i);
                for (int j = 0; j < width; j++)
                    if (state[i, j] == false)
                        Console.Write("\t.");
                    else Console.Write("\t" + index[i, j]);
                Console.WriteLine();
            }
        }
    }
}
