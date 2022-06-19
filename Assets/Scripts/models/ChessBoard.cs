using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Current;
    public GameObject Go;
    public GameObject cellPrefap;
    public LayerMask CellLayerMask = 0;
    private Cell _curentHoverCell = null;
    private Cell _currentSelectedCell = null;
    private float cell_size = -1;
    public float CELL_SIZE {
        get
        {
            if (cell_size < 0)
                cell_size = cellPrefap.GetComponent<Cell>().SIZE;
            return cell_size;
        }
    }
     void Awake()
    {
        Current = this;
    }
     void Start()
    {
        InitChessBoard();
       
    }
    
    //góc t?a ??
    private Vector3 basePotision = Vector3.zero;
    private Cell[][] _cells;
    public Cell[][] cells { get { return _cells; } }
    private List<BasePiece> pieces;

    [ContextMenu("Check")]
    public void Check()
    {
        InitChessBoard();
    }
    void Update()
    {
        if (BaseGameController.current.GameState == EgameState.PLAYING)
        {
            CheckUserInput();
        }
    }

     void OnMouseDown()
    {
        if(_curentHoverCell != null)
        {
            Debug.Log(_curentHoverCell.name);
        }    
    }

    private void CheckUserInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
        {
            Debug.DrawLine(ray.origin, hit.point);
            // Debug.Log(hit.collider.name);

            Cell newcell = hit.collider.GetComponent<Cell>();
            if (newcell != _curentHoverCell)
            {
                if (_curentHoverCell != null)
                    _curentHoverCell.SetCellState(Ecellstate.NORMAL);
                _curentHoverCell = newcell;
                _curentHoverCell.SetCellState(Ecellstate.HOVER);
            }
       
        }
        else
        {
            if (_curentHoverCell != null)
            {
                _curentHoverCell.SetCellState(Ecellstate.NORMAL);
                _curentHoverCell = null;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (_curentHoverCell != null)
            {
                if (_currentSelectedCell != null)
                    _currentSelectedCell.SetCellState(Ecellstate.UNSELECTED);
                _currentSelectedCell = _curentHoverCell;
                _currentSelectedCell.SetCellState(Ecellstate.SELECTED);

                Debug.Log(_currentSelectedCell.CurrentPiece.info.Name);
            }
        }
    }
    [ContextMenu("InitChessBoard")]
    public void InitChessBoard()
    {
        basePotision = Vector3.zero + new Vector3(-3.5f * CELL_SIZE, 0, 0);
        _cells = new Cell[8][];
        for (int i = 0; i < 8; i++)
            _cells[i] = new Cell[8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                GameObject c = GameObject.Instantiate(cellPrefap, CanculatePotision(i, j), Quaternion.identity) as GameObject;
                c.transform.parent = this.transform.GetChild(0);
                _cells[j][i] = c.GetComponent<Cell>();
                if ((i + j) % 2 == 0)
                {
                    _cells[j][i].Color = Ecellcollor.WHITE;

                }
                else _cells[j][i].Color = Ecellcollor.BLACK;
            }
        }
    }
    [ContextMenu("InitChessPieces")]
    public void InitChessPieces()
    {
        pieces = new List<BasePiece>();
        

        List<PiecesInfor> listinfo = new List<PiecesInfor>();
        //white
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn1", Path = "Pieces/WhitePawn", X = 1, Y = 0 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn2", Path = "Pieces/WhitePawn", X = 1, Y = 1 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn3", Path = "Pieces/WhitePawn", X = 1, Y = 2 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn4", Path = "Pieces/WhitePawn", X = 1, Y = 3 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn5", Path = "Pieces/WhitePawn", X = 1, Y = 4 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn6", Path = "Pieces/WhitePawn", X = 1, Y = 5 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn7", Path = "Pieces/WhitePawn", X = 1, Y = 6 });
        listinfo.Add(new PiecesInfor() { Name = "WhitePawn8", Path = "Pieces/WhitePawn", X = 1, Y = 7 });

        listinfo.Add(new PiecesInfor() { Name = "WhiteRook1", Path = "Pieces/WhiteRook", X = 0, Y = 0 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteRook2", Path = "Pieces/WhiteRook", X = 0, Y = 7 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteKnight1", Path = "Pieces/WhiteKnight", X = 0, Y = 1 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteKnight2", Path = "Pieces/WhiteKnight", X = 0, Y = 6 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteBishop1", Path = "Pieces/WhiteBishop", X = 0, Y = 2 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteBishop2", Path = "Pieces/WhiteBishop", X = 0, Y = 5 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteKing1", Path = "Pieces/WhiteKing", X = 0, Y = 3 });
        listinfo.Add(new PiecesInfor() { Name = "WhiteKing2", Path = "Pieces/WhiteQueen", X = 0, Y = 4 });

        //Black
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn1", Path = "Pieces/BlackPawn", X = 6, Y = 0 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn2", Path = "Pieces/BlackPawn", X = 6, Y = 1 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn3", Path = "Pieces/BlackPawn", X = 6, Y = 2 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn4", Path = "Pieces/BlackPawn", X = 6, Y = 3 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn5", Path = "Pieces/BlackPawn", X = 6, Y = 4 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn6", Path = "Pieces/BlackPawn", X = 6, Y = 5 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn7", Path = "Pieces/BlackPawn", X = 6, Y = 6 });
        listinfo.Add(new PiecesInfor() { Name = "BlackPawn8", Path = "Pieces/BlackPawn", X = 6, Y = 7 });

        listinfo.Add(new PiecesInfor() { Name = "BlackRook1", Path = "Pieces/BlackRook", X = 7, Y = 0 });
        listinfo.Add(new PiecesInfor() { Name = "BlackRook2", Path = "Pieces/BlackRook", X = 7, Y = 7 });
        listinfo.Add(new PiecesInfor() { Name = "BlackKnight1", Path = "Pieces/BlackKnight", X = 7, Y = 1 });
        listinfo.Add(new PiecesInfor() { Name = "BlackKnight2", Path = "Pieces/BlackKnight", X = 7, Y = 6 });
        listinfo.Add(new PiecesInfor() { Name = "BlackBishop1", Path = "Pieces/BlackBishop", X = 7, Y = 2 });
        listinfo.Add(new PiecesInfor() { Name = "BlackBishop2", Path = "Pieces/BlackBishop", X = 7, Y = 5 });
        listinfo.Add(new PiecesInfor() { Name = "BlackKing1", Path = "Pieces/BlackKing", X = 7, Y = 3 });
        listinfo.Add(new PiecesInfor() { Name = "BlackKing2", Path = "Pieces/BlackQueen", X = 7, Y = 4 });
        foreach (var info  in listinfo)
        {
            GameObject Go = GameObject.Instantiate<GameObject>(ResourceController.Instance.GetGameObject(info.Path));
            Go.transform.parent = this.transform.GetChild(1);
            Go.name = info.Name;
             
            BasePiece p = Go.GetComponent<BasePiece>();
            p.SetInfo(info);
            pieces.Add(p);

            _cells[info.X][info.Y].SetPieces(p); 
        }
       
    }
    
    public Vector3 CanculatePotision(int i, int j)
    {
        return basePotision + new Vector3(i * CELL_SIZE, 0, j * CELL_SIZE);
    }    
}
