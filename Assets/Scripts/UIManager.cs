using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject m_Player1InputSystem;
    [SerializeField]
    private GameObject m_Player2InputSystem;

    [SerializeField]
    private Button[] m_CharacterButtons;

    private bool player1Selected = false;
    private bool player2Selected = false;

    public enum EScene
    {
        MainMenu,
        CharacterSelection,
        InGame
    };

    [SerializeField]
    public EScene activeScene;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (activeScene == EScene.CharacterSelection)
        {
            m_Player1InputSystem.SetActive(true);
            m_Player2InputSystem.SetActive(false);
        }

    }

    private void Update()
    {
        if (activeScene == EScene.CharacterSelection)
        {

            if (player1Selected)
                m_CharacterButtons[(int)gameManager.player1].interactable = false;
            if (player2Selected)
                m_CharacterButtons[(int)gameManager.player2].interactable = false;


            if (gameManager.player1 == GameManager.ECharacters.notSelected)
            {
                player1Selected = false;
                m_Player1InputSystem.SetActive(true);
                m_Player2InputSystem.SetActive(false);

                return;
            }
            else if (gameManager.player2 == GameManager.ECharacters.notSelected)
            {
                player2Selected = false;
                m_Player1InputSystem.SetActive(false);
                m_Player2InputSystem.SetActive(true);
                return;
            }
        }
    }

    public void SelectCharacter(int _char)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player1Selected && !player2Selected)
            {
                gameManager.player1 = (GameManager.ECharacters)_char;
                Debug.Log($"Player 1 chose: {gameManager.player1}");
                player1Selected = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (player1Selected && !player2Selected)
            {
                gameManager.player2 = (GameManager.ECharacters)_char;
                Debug.Log($"Player 2 chose: {gameManager.player2}");
                player2Selected = true;
            }
        }
    }

    public void LoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);   
    }
}
