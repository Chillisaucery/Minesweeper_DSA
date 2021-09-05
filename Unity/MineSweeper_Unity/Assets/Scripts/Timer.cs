using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public WinLoseController controller;
    public TMP_Text text;

    float miliSec = 0;

    bool canRun = false;

    private void Update()
    {

        if (controller.CheckIfPlayable())
        {
            miliSec += Time.deltaTime;
            Debug.Log("Updated " + canRun + " " + miliSec);

            UpdateText();
        }
    }

    private void UpdateText()
    {
        text.text = "";

        if (miliSec < 100)
            text.text += "0";

        if (miliSec < 10)
            text.text += "0";

        text.text += "" + (int) miliSec;
    }

    public void SetRun(bool canRun)
    {
        this.canRun = canRun;
    }

    public void Reset()
    {
        this.canRun = false;
        this.miliSec = 0;
    }


}
