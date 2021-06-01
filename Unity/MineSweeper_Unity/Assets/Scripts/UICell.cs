using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICell : MonoBehaviour, IPointerClickHandler
{
    private int x;
    private int y;

    public char value = 'c';
    public bool isFlagged= false;

    const int UNIT = 100;
    private UIBoard uiBoard;
    public RectTransform rectTranform;
    private Image cellImage;

    public Sprite no1;
    public Sprite no2;
    public Sprite no3;
    public Sprite no4;
    public Sprite no5;
    public Sprite no6;
    public Sprite no7;
    public Sprite no8;
    public Sprite no9;
    public Sprite empty;
    public Sprite mine;
    public Sprite flagged;
    private Sprite defaultSprite;
    


    // Start is called before the first frame update
    void Start()
    {
        uiBoard = FindObjectOfType<UIBoard>();
        cellImage = GetComponent<Image>();
        defaultSprite = cellImage.sprite;
    }

    public void SelectThisCell()
    {
        //Exit this function if it is flagged, prevent the player from selecting it
        if (isFlagged)
            return;
        
        value = uiBoard.GetValueAt(x, y);
        
        // if player select the cell normally, that is to open a concealed cell
        if (value == 'c')
        {
            uiBoard.SelectCell(x, y);
            UpdateCellUI(false);
        }

        //if player select a cell that is already opened and has at least a flag in 8 cells around it
        if (value != 'c')
        {
            if (uiBoard.CheckFlagAroundCell(x, y))
            {
                uiBoard.SelectCellWithFlagAround(x, y);
            }
                
        }

    }

    public void SelectCellWithFlagAround()
    {
        if (!isFlagged)
            uiBoard.SelectCell(x,y);

        UpdateCellUI(false);
    }

    public void UpdateCellUI(bool isFinalExpansion)
    {
        value = uiBoard.GetValueAt(x, y);

        switch (value)
        {
            case '1':
                cellImage.sprite = no1;
                break;
            case '2':
                cellImage.sprite = no2;
                break;
            case '3':
                cellImage.sprite = no3;
                break;
            case '4':
                cellImage.sprite = no4;
                break;
            case '5':
                cellImage.sprite = no5;
                break;
            case '6':
                cellImage.sprite = no6;
                break;
            case '7':
                cellImage.sprite = no7;
                break;
            case '8':
                cellImage.sprite = no8;
                break;
            case '9':
                cellImage.sprite = no9;
                break;
            case 'X':
                cellImage.sprite = mine;
                break;
            case ' ':
                cellImage.sprite = empty;
                if (!isFinalExpansion)
                    uiBoard.UpdateAllCellUI();
                break;
            case 'c':   //'c' for concealed
                if (isFlagged)
                    cellImage.sprite = flagged;
                else
                    cellImage.sprite = defaultSprite;
                break;
            default:
                break;
        }
     
    } 

    public void Locate()
    {

        float posiX = x * UNIT;
        float posiY = y * UNIT;
        rectTranform.localPosition = new Vector2(posiX, posiY);
    }

    public void SetCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public (int, int) GetCoordinate()
    {
        return (x, y);
    }

    public void OnPointerClick(PointerEventData eventData)
    { 
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            MarkFlag();
        }

        UpdateCellUI(true);
    }

    public void MarkFlag()
    {
        value = uiBoard.GetValueAt(x, y);
        if (value == 'c')
            isFlagged = !isFlagged;
        else isFlagged = false;

        Debug.Log("Is flagged: " + isFlagged);
    }
}
