using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.classes
{
    /// <summary>
    /// State Board contains the state of each cell of the board (revealed or not revealed)
    /// Contributors: Chillisauce 23 April
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
            return (this.stateBoard);
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

        public StateBoard(int height, int width, bool[,] stateBoard)
        {
            this.height = height;
            this.width = width;
            this.stateBoard = new bool[height, width];

            //Deep copy the stateboard from the param into this stateboard
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    this.stateBoard[i, j] = stateBoard[i, j];
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
            //Variables
            //This boolean 2D array represent the cells that has been opened during the a call of this function.
            //One array is generated per call and used only in that call
            bool[,] ifOpened = new bool[height, width];

            //This is the diameter of the cell expansion.
            int expandingRadius = 0;

            //A variable to check if the expansion can further occur
            bool canExpand = false;

            //Reveal the target cell
            stateBoard[x, y] = true;
            if (indexBoard[x,y] == ' ') //If the target cell is empty
                {
                ifOpened[x, y] = true;  //Set the origin of the expansion to true, allowing the expansion to occur
                canExpand = true;
                }
                
            //Revealing the surrounding cells
            while ( canExpand ) // stop expanding when the radius reach half of the height or width
            {
                expandingRadius++; //Expand the radius
                canExpand = false;

                //Looping through the area within radius
                for (int i = x - expandingRadius; i <= x + expandingRadius; i++)
                    for (int j = y - expandingRadius; j <= y + expandingRadius; j++)
                        if ((i < height) && (j < width) && (i >= 0) && (j >= 0) )
                        {
                            //Check the 8 surrounding cell to know if there were any cells opened (satisfied if there was)
                            //Check if this cells is empty or not (satisfied if empty)
                            //Check if this cells is opened or not (satisfied if not opened yet)
                            //If all the conditions are satisfied, reveal it and its 8 surrounding cells
                            if (MathFunc.CheckSurrounding(ifOpened, height, width, i, j) && indexBoard[i, j] == ' '
                                && ifOpened[i,j] == false)
                            {
                                RevealSquare(i, j);
                                ifOpened[i, j] = true;  //Mark the cell as opened
                                //If there is at least 1 cell opened in this loop, the expansion can continue
                                //Otherwise, it cannot
                                canExpand = true;   
                            }      
                        }
            }
            
        }


        //Input a position of a stateBoard, reveal the cell at that position and the 8 cells surrounding it.
        /// <param name="x"> Coordinate x </param>
        /// <param name="y"> Coordinate y </param>
        public void RevealSquare(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                    if ((i >= 0) && (i <= height - 1) && (j >= 0) && (j <= width - 1))
                    {
                        stateBoard[i, j] = true;
                    }
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
                    else Console.Write(" ");
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
        

        //The win condition.
        public bool winCondition(int mines){
            int count = 0;
            for(int i = 0; i < height; i++){
                for(int j = 0; j < width; j++){
                    if(stateBoard[i, j] == true){
                        count++;
                    }
                }
            }
            if(count == ((width * height) - mines)){
                Console.Write("You win\n");
                return false;
            };
            return true;
        }
        //lose condition
        public bool losecondition(int x,int y,char[,] indexBoard) {
        bool lose = false;
         for(int i = 0; i < height; i++){
                for(int j = 0; j < width; j++){
                    if(indexBoard[x,y] == 'X' && stateBoard[i, j] == true ){
                        lose = true;
                    }
                }
            }
            return lose;
    }

        //Deep copy a StateBoard object
        public StateBoard Clone()
        {
            return new StateBoard(height, width, stateBoard);    
        }
        
    }
    
}

