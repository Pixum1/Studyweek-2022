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

    public Character[] Characters;

    public ECharacters Player1Char = ECharacters.notSelected;
    public ECharacters Player2Char = ECharacters.notSelected;

    [HideInInspector] public PlayerController Player1;
    [HideInInspector] public PlayerController Player2;

    [HideInInspector] public int P1FinalHealth;
    [HideInInspector] public int P2FinalHealth;

    [HideInInspector] public UIManager UImanager;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (UImanager == null)
            UImanager = FindObjectOfType<UIManager>();

        if (UImanager.activeScene == UIManager.EScene.CharacterSelection)
        {
            if (Player1Char != ECharacters.notSelected && Player2Char != ECharacters.notSelected)
            {
                Debug.Log("All characters selected");
                UImanager.LoadScene(2);
            }
        }
        if (UImanager.activeScene == UIManager.EScene.InGame)
        {
            if (Player1 == null)
                Player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
            if (Player2 == null)
                Player2 = GameObject.Find("Player2").GetComponent<PlayerController>();

            if (Player1.Health <= 0 || Player2.Health <= 0)
            {
                P1FinalHealth = Player1.Health;
                P2FinalHealth = Player2.Health;
                UImanager.LoadScene(3); //Load Game Over Scene
            }
        }
    }

    public void DamagePlayer1()
    {
        Player1.Health--;
    }
    public void DamagePlayer2()
    {
        Player2.Health--;
    }
}
