using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer spr;
    private GameObject go;
    private BoxCollider2D col;
    public int HP = 1;

    public void Init(GameObject _go, Vector2Int _position, Vector2Int _dimensions, Sprite _sprite)
    {
        go = _go;
        //go.transform.localScale = new Vector3(_dimensions.x, _dimensions.y);
        go.transform.position = new Vector3(_position.x, _position.y);
        spr = go.AddComponent<SpriteRenderer>();
        spr.sprite = _sprite;
        col = go.AddComponent<BoxCollider2D>();
    }

    public void GetDamage()
    {
        HP--;
    }

    public void ActivateObstacle()
    {
        go.SetActive(true);
    }
    public void DeactivateObstacle()
    {
        go.SetActive(false);
    }
}
