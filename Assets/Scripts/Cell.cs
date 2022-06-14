using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int Position;
    public enum EStatus
    {
        alive,
        dead
    };
    public EStatus Status;
    public Obstacle Obstacle;

    public Cell(EStatus _status, Vector2Int _position)
    {
        Status = _status;
        Position = _position;
    }
}
