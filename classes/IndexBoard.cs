using System;
using System.Collections.Generic;
using System.Drawing;

namespace Minesweeper.classes
{
    /// <summary>
    /// Index board contains the index values of the board (the numbers 1,2,3,4,..., having mines or not having mines)
    /// Contributors: Chillisauce 20 April
    /// </summary>

    public class IndexBoard
    {
        //Variables: Height and width
        private int height;
        private int width;

        //Variables: Mine number
        private int mineNum;

        //The 2D array to contain the letters that represent the board, each element is a character
        //Cells with mine: "X"; cell with no mine: " "; cells with number is represented with that number (1: "1", 2: "2", 3: "3", etc.)
        private char[,] indexBoard;

        //Getter
        public char[,] getIndexBoard()
        {
            return (indexBoard);
        }

        //Constructor, it will create a a blank 2D array indexBoard
        public IndexBoard(int height, int width, int mineNum)
        {
            //Initial the board
            indexBoard = new char[height, width];

            //Record the height, width and mineNum
            this.height = height;
            this.width = width;
            this.mineNum = mineNum;
        }

        //Fill the board with " "
        private void fillEmpty()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    indexBoard[i, j] = ' ';
                }
        }

        //Fill the board with mines, base on the given mine numbers, height and width
        private void fillMines()
        {
            int counter = 0; //Count the number of mines existing in the board
            int probablity = 100 * mineNum / (height * width); //Probablity for a mines to exist in a cell

            //Repeatedly fill the board with mines until the board has enough mines
            while (counter < mineNum)
            {
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        //Mine placing
                        if (MathFunc.RandomWithProbability(probablity) == true && counter < mineNum)
                        {
                            indexBoard[i, j] = 'X'; //Place a mine
                            counter++; //Update the number of mines
                        }
            }
        }

        //Calculate the number value of a cell that surrounding a mines
        private void fillNumber()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    int surroundingMine = 0;
                    
                    //If the cells is not a mine 
                    if (indexBoard[i, j] != 'X')
                        //Check for the surrounding cells and record the number of mines surrounding
                        for (int k = i - 1; k <= i + 1; k++)
                            for (int h = j - 1; h <= j + 1; h++)
                            {
                                if ((k < height) && (h < width) && (k >= 0) && (h >= 0))
                                    if (indexBoard[k, h] == 'X')
                                        surroundingMine++;
                            }

                    //Record the number of mines
                    if (surroundingMine > 0)
                        indexBoard[i, j] = surroundingMine.ToString()[0];
                }
        }

        //Display the indexBoard
        public void displayIndexBoard()
        {
            Console.WriteLine("The index board is: ");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(indexBoard[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Fill the values into the board.
        public void createIndexBoard()
        {
            this.fillEmpty();
            this.fillMines();
            this.fillNumber();
            this.displayIndexBoard();
        }


    }
}