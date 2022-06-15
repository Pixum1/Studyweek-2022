using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum ECharacters
    {
        notSelected = -1,
        char1,
        char2,
        char3,
        char4,
        char5
    };

    public ECharacters player1;
    public ECharacters player2;

    public UIManager uimanager;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(uimanager == null)
            uimanager = FindObjectOfType<UIManager>();

        if (uimanager.activeScene == UIManager.EScene.CharacterSelection)
        {
            if (player1 != ECharacters.notSelected && player2 != ECharacters.notSelected)
            {
                Debug.Log("All characters selected");
                uimanager.LoadScene(2);
            }
        }
    }
}
