using Minesweeper.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : MonoBehaviour
{
    IndexStateConnecter isc;    //isc is shortterm for index state connector

    //Reference to game objects
    public GameObject cellPrefab;
    public GameObject boardCanvas;
    public WinLoseController winLoseController;
    private CameraController cameraController;
    private Timer timer;

    GameObject[,] cellList;

    public TMP_InputField heightInput;
    public TMP_InputField widthInput;
    public TMP_InputField mineNumInput;

    private int height = 10;
    private int width = 10;
    private int mineNum = 10;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        timer = FindObjectOfType<Timer>();

        StartGame();

        cameraController.SetPositionConstrain(cellList[0, width - 1], cellList[height - 1, 0]);
    }

    void StartGame()
    {
        timer.Reset();
        timer.SetRun(true);

        cameraController.SetDeviant((height*width)/2);

        isc = new IndexStateConnecter(height, width, mineNum);

        cellList = new GameObject[height, width];

        //Fill the board with values
        isc.TestBoard();

        //Start the game
        isc.StartGame();

        //Draw the board onto the UI panel
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                cellList[i, j] = Instantiate(cellPrefab, boardCanvas.GetComponent<RectTransform>());   //Instantiate a cell and save it into the array

                cellList[i, j].GetComponent<UICell>().SetCoordinate(i, j);  //Set the x and y of the cell

                //Locate the cell in the world space
                cellList[i, j].GetComponent<UICell>().Locate();
            }

        //Locate the camera to the center cell
        cameraController.SetOrigin(cellList[height / 2, width / 2]);
    }

    public void SelectCell(int x, int y)
    {

        if (winLoseController.CheckIfPlayable())
        {
            isc.SelectCell(x, y);
            CheckWinLose();
        }
    }

    public void SelectCellWithFlagAround(int x, int y)
    {
        for (int i = x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
                if (!((i == x) && (j == y)))
                    try
                    {
                        cellList[i, j].GetComponent<UICell>().SelectCellWithFlagAround();
                    }
                    catch
                    {

                    }
    }

    public bool CheckFlagAroundCell(int x, int y)
    {
        for (int i = x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
                if (!((i == x) && (j == y)))
                    try
                    {
                        if (cellList[i, j].GetComponent<UICell>().isFlagged)
                            return (true);
                    }
                    catch
                    {

                    }
        return false;
    }

    private void CheckWinLose()
    {
        bool isWon = false;
        bool isLost = false;

        isLost = isc.stateBoard.LoseCondition(isc.indexBoard.getIndexBoard());
        isWon = isc.stateBoard.WinCondition(mineNum);

        winLoseController.SetLose(isLost);
        winLoseController.SetWin(isWon);
    }

    public void UpdateAllCellUI()
    {
        foreach (GameObject cell in cellList)
        {
            cell.GetComponent<UICell>().UpdateCellUI(true);
        }
    }

    public char GetValueAt(int x, int y)
    {
        return (isc.valueBoard()[x, y]);
    }

    public void Undo()
    {
        isc.UndoFunc();
        UpdateAllCellUI();
        CheckWinLose();
    }

    public void Redo()
    {
        isc.RedoFunc();
        UpdateAllCellUI();
        CheckWinLose();
    }

    public void ResetPlay()
    {
        //Destroy all cells
        foreach (GameObject cell in cellList)
            Destroy(cell);

        StartGame();
        winLoseController.ResetWinLose();
    }



    public void SetHeight()
    {
        try
        {
            this.height = Int16.Parse(heightInput.text);
        }
        catch
        {

        }
    }

    public void SetWidth()
    {
        try
        {
            this.width = Int16.Parse(widthInput.text);
        }
        catch
        {

        }
    }

    public void SetMineNum()
    {
        try
        {
            this.mineNum = Int16.Parse(mineNumInput.text);
        }
        catch
        {

        }
    }
}
