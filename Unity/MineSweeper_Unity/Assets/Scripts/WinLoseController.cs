using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseController : MonoBehaviour
{
    private bool isWon = false;
    private bool isLost = false;

    public TMP_Text text;
    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    public void SetWin(bool isWon)
    {
        this.isWon = isWon;
        SetText();

        if (isWon)
            timer.SetRun(false);
    }
    public void SetLose(bool isLost)
    {
        this.isLost = isLost;
        SetText();
        
        if (isLost)
            timer.SetRun(false);
    }

    public bool GetIsWon()
    {
        return isWon;
    }

    public bool GetIsLost()
    {
        return isLost;
    }

    public bool CheckIfPlayable()
    {
        return (!GetIsLost() && !GetIsWon());
    }

    public void ResetWinLose()
    {
        this.isLost = false;
        this.isWon = false;
        timer.SetRun(false);
    }

    public void SetText()
    {
        if (isLost)
        {
            text.text = "Lost";
            return;
        }

        if (isWon)
        {
            text.text = "Won";
            return;
        }

        text.text = "Playing";
    }
}
