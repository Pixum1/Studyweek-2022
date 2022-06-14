using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private enum EPlayerInputType
    {
        player1,
        player2,
        ai
    };

    [SerializeField]
    private EPlayerInputType playerType;

    private string vertAxis;

    private void Start()
    {
        switch (playerType)
        {
            case EPlayerInputType.player1:
                vertAxis = "Vertical";
                break;
            case EPlayerInputType.player2:
                vertAxis = "Vertical2";
                break;
            case EPlayerInputType.ai:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        float vertPos = Input.GetAxisRaw(vertAxis);

        if (vertPos != 0)
        {
            transform.position += Vector3.up * (vertPos * speed * Time.deltaTime);
        }
    }
}
