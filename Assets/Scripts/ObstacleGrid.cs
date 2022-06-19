using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGrid : MonoBehaviour
{
    [SerializeField]
    private Vector2Int m_BlockSize;
    [SerializeField]
    private Vector2Int m_StartPosition;
    [SerializeField]
    private int m_Height;
    [SerializeField]
    private int m_Width;
    private Cell[] cells;
    [SerializeField]
    private Sprite[] m_Sprites;

    private void Start()
    {
        InitializeGrid();
    }

    private void Update()
    {
        UpdateCells();
    }

    private void InitializeGrid()
    {
        cells = new Cell[m_Height * m_Width];

        for (int y = 0, i = 0; y < m_Height * m_BlockSize.y; y += m_BlockSize.y)
        {
            for (int x = 0; x < m_Width * m_BlockSize.x; x += m_BlockSize.x, i++)
            {
                Vector2Int pos = new Vector2Int(x + m_StartPosition.x, y + m_StartPosition.y);
                cells[i] = new Cell(Cell.EStatus.alive, pos);

                GameObject go = new GameObject("Cell: " + i);
                go.transform.SetParent(this.transform);
                cells[i].Obstacle = go.AddComponent<Obstacle>();
                cells[i].Obstacle.Init(go, cells[i].Position, m_Sprites);
            }
        }
    }

    private void UpdateCells()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].Obstacle.HP <= 0)
                cells[i].Status = Cell.EStatus.dead;

            switch (cells[i].Status)
            {
                case Cell.EStatus.alive:
                    cells[i].Obstacle.ActivateObstacle();
                    break;
                case Cell.EStatus.dead:
                    if (!cells[i].Obstacle.CoroutineCalled)
                    {
                        cells[i].Obstacle.DeactivateObstacle();
                        cells[i].Obstacle.StartCoroutine(cells[i].Obstacle.RespawnBlock(7f, cells[i]));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (cells != null)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(new Vector3(cells[i].Position.x, cells[i].Position.y), new Vector3(m_BlockSize.x, m_BlockSize.y));
            }
        }
    }
}
