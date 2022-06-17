using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer spr;
    private GameObject go;
    private BoxCollider2D col;
    public int HP = 1;
    public bool CoroutineCalled = false;
    private Sprite[] sprites;

    public void Init(GameObject _go, Vector2Int _position, Sprite[] _sprites)
    {
        sprites = _sprites;
        go = _go;
        go.transform.position = new Vector3(_position.x, _position.y);
        spr = go.AddComponent<SpriteRenderer>();
        HP = Random.Range(1, sprites.Length+1);
        spr.sprite = sprites[HP-1];
        col = go.AddComponent<BoxCollider2D>();
    }

    public IEnumerator RespawnBlock(float _seconds, Cell _cell)
    {
        CoroutineCalled = true;
        yield return new WaitForSeconds(_seconds);
        HP = Random.Range(1, sprites.Length);
        spr.sprite = sprites[HP-1];
        _cell.Status = Cell.EStatus.alive;
        ActivateObstacle();
        CoroutineCalled = false;
    }

    public void GetDamage()
    {
        HP--;
        if(HP > 0)
            spr.sprite = sprites[HP-1];
    }

    public void ActivateObstacle()
    {
        spr.enabled = true;
        col.enabled = true;
    }
    public void DeactivateObstacle()
    {
        spr.enabled = false;
        col.enabled = false;
    }
}
