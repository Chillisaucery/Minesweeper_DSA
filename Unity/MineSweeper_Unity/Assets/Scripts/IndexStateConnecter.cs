using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Minesweeper.classes
{
    /// <summary>
    /// The board is the whole gameplay. It receives input of the coordination of x and y, processing the logic and then display the output into the screen.
    /// It contains and link together indexBoard, stateBoard and UIBoard.
    /// Contributors: Chillisauce 20 April
    /// </summary>
    public class IndexStateConnecter
    {
        //Components
        public IndexBoard indexBoard;
        public StateBoard stateBoard;
        //private UIBoard uiBoard;

        //Variables
        private int height;
        private int width;
        private int mineNum;


        Stack<StateBoard> undoStack = new Stack<StateBoard>();
        Stack<StateBoard> redoStack = new Stack<StateBoard>();

        /// <summary>
        /// Constructor
        /// With given params, it initialise (creates new blank) indexBoard and stateBoard
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="mineNum"></param>
        public IndexStateConnecter(int height, int width, int mineNum)
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

        // Start the game, create new board
        public void StartGame()
        {
            this.indexBoard.createIndexBoard();
            this.stateBoard.createStateBoard();

            undoStack.Push(new StateBoard(height, width, stateBoard.getStateBoard()));

        }

        public void SelectCell(int x, int y)
        {
            stateBoard.revealCell(x, y, indexBoard.getIndexBoard());
            undoStack.Push(stateBoard.Clone());
            redoStack.Clear();
            stateBoard.displayStateBoard();
        }


        //Undo function and Try not to make stack be empty
        public void UndoFunc()
        {
            try
            {
                redoStack.Push(undoStack.Pop().Clone());
                this.stateBoard = undoStack.Peek().Clone();

                Debug.Log("Undo");
            }
            catch (Exception) { }
        }

        // Redo Function and Try not to make stack be empty 
        public void RedoFunc()
        {
            try
            {
                this.stateBoard = redoStack.Peek().Clone();
                undoStack.Push(redoStack.Pop().Clone());
            }
            catch (Exception)
            {

            }

        }

        // Display the board to the console
        public char[,] valueBoard()
        {
            char[,] output = new char[height, width];

            char[,] index = indexBoard.getIndexBoard();
            bool[,] state = stateBoard.getStateBoard();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    if (state[i, j] == false)
                        output[i, j] = 'c';
                    else output[i, j] = index[i, j];
            }

            return output;
        }
    }
}
