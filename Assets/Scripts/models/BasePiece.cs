using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePiece: MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetPotision;
    public PiecesInfor info { get; private set; }
    [SerializeField]
    protected Eplayer _player;
    public Eplayer Player { get { return _player; } protected set { _player = value; } }
    private Vector2 _originalLocation;
    public Vector2 Location { get; private set; }
    public void SetInfo(PiecesInfor info)
    {
        _originalLocation = new Vector2(info.X , info.Y);
        this.info = info;
        this.transform.position = offsetPotision + new Vector3(info.Y * ChessBoard.Current.CELL_SIZE, 0, info.X * ChessBoard.Current.CELL_SIZE); 
    }
    public abstract void Move();
}
