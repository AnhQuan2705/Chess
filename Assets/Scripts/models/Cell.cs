using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    private Transform cellHoverObj;
    private Ecellcollor _color;
    private Transform CellSelectedObj;
    public BasePiece CurrentPiece { get;  private set; }
    public Ecellcollor Color
    {
        get { return _color; }
        set { 
            _color = value;
            switch (_color)
            {
                case Ecellcollor.BLACK:
                    GetComponent<Renderer>().material = ResourceController.Instance.BlackCellMaterial;
                    break;
                case Ecellcollor.WHITE:
                    GetComponent<Renderer>().material = ResourceController.Instance.WhiteCellMaterial;
                    break;
                default:
                    break;
            }
        }
    }
    private Ecellstate _state;
    public Ecellstate State
    {
        get { return _state; }
        private set
        {
            _state = value;

            switch (_state)
            {
                case Ecellstate.NORMAL:
                    cellHoverObj.gameObject.SetActive(false);
                    CellSelectedObj.gameObject.SetActive(false);
                    break;
                case Ecellstate.HOVER:
                    cellHoverObj.gameObject.SetActive(true);
                    CellSelectedObj.gameObject.SetActive(false);
                    break;
                case Ecellstate.SELECTED:
                    cellHoverObj.gameObject.SetActive(false);
                    CellSelectedObj.gameObject.SetActive(true);
                    break;
                case Ecellstate.TARGETED:
                    break;
                default:
                    cellHoverObj.gameObject.SetActive(false);
                    CellSelectedObj.gameObject.SetActive(false);
                    break;
            }
        }
    }
     
    public float SIZE
    {
        get
        {
            return GetComponent<Renderer>().bounds.size.x;
        }
    }
    
    [ContextMenu("Check")]
    public void check()
    {
        Debug.Log("SIZE : " + SIZE);
    }

     void Awake()
    {
        cellHoverObj = this.transform.GetChild(0);
        CellSelectedObj = this.transform.GetChild(1);
    }
    void Start()
    {
       
       // State = Ecellstate.NORMAL;
    }
    
    protected void OnMouseDown()
    {
        SetCellState(Ecellstate.SELECTED);
    }
    protected void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        if (State != Ecellstate.SELECTED)
            State = Ecellstate.HOVER;
    }
    protected void OnMouseExit()
    {  
        Debug.Log("OnMouseExit");
        if (State != Ecellstate.SELECTED)
            State = Ecellstate.NORMAL;
    }
    public void SetCellState(Ecellstate cellState)
    {
        if(cellState != Ecellstate.SELECTED)
        {
            if (this.State != Ecellstate.SELECTED)
                this.State = cellState;
        }
        else
        {
            if(this.State == Ecellstate.SELECTED)
            {
                this.State = Ecellstate.HOVER;
                
            }  
            else
            {
                this.State = Ecellstate.SELECTED;
            }    
        }

        if (cellState == Ecellstate.UNSELECTED)
        {
            this.State = Ecellstate.UNSELECTED;
        }
    }

    public void SetPieces(BasePiece piece)
    {
        this.CurrentPiece =  piece;
    }    
}
