using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu References")]
    [SerializeField]
    private GameManager m_GameManager;

    [Header("Character Selection References")]
    [SerializeField]
    private GameObject m_Player1InputSystem;
    [SerializeField]
    private GameObject m_Player2InputSystem;
    [SerializeField]
    private Button[] m_CharacterButtons;

    private bool player1Selected = false;
    private bool player2Selected = false;

    [Header("InGame References")]
    [SerializeField]
    private TMP_Text m_Countdown;
    [SerializeField]
    private RawImage m_Player1Character;
    [SerializeField]
    private RawImage m_Player2Character;
    public Animator m_Player1Animator;
    public Animator m_Player2Animator;

    [Header("Game Over References")]
    [SerializeField]
    private TMP_Text m_PlayerWonText;
    [SerializeField]
    private RawImage m_WinningCharacter;
    [SerializeField]
    private RawImage m_LosingCharacter;

    [Header("Scene Transition References")]
    [SerializeField]
    private Animator m_Transitions;


    public enum EScene
    {
        MainMenu,
        CharacterSelection,
        InGame,
        GameOver
    };

    [SerializeField]
    public EScene activeScene;

    private void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();

        if (activeScene == EScene.CharacterSelection)
        {
            m_Player1InputSystem.SetActive(true);
            m_Player2InputSystem.SetActive(false);
        }

        if(activeScene == EScene.InGame)
        {
            StartCoroutine(Countdown());
        }
    }

    private void Update()
    {
        if (activeScene == EScene.CharacterSelection)
        {
            if (player1Selected)
                m_CharacterButtons[(int)m_GameManager.Player1Char].interactable = false;
            if (player2Selected)
                m_CharacterButtons[(int)m_GameManager.Player2Char].interactable = false;


            if (m_GameManager.Player1Char == GameManager.ECharacters.notSelected)
            {
                player1Selected = false;
                m_Player1InputSystem.SetActive(true);
                m_Player2InputSystem.SetActive(false);

                return;
            }
            else if (m_GameManager.Player2Char == GameManager.ECharacters.notSelected)
            {
                player2Selected = false;
                m_Player1InputSystem.SetActive(false);
                m_Player2InputSystem.SetActive(true);
                return;
            }
        }
        else if (activeScene == EScene.InGame)
        {
            if (m_GameManager.Player1.Health > 0 && m_GameManager.Player2.Health > 0)
            {
                m_Player1Character.texture = m_GameManager.Characters[(int)m_GameManager.Player1Char].CharacterSprites[m_GameManager.Player1.Health - 1];
                m_Player2Character.texture = m_GameManager.Characters[(int)m_GameManager.Player2Char].CharacterSprites[m_GameManager.Player2.Health - 1];
            }

            //if game over delete gamemangaer and load scene 0
        }
        else if (activeScene == EScene.GameOver)
        {
            if (m_GameManager.Player2.Health <= 0)
            {
                m_WinningCharacter.texture = m_GameManager.Characters[(int)m_GameManager.Player1Char].CharacterSprites[m_GameManager.P1FinalHealth - 1];
                m_LosingCharacter.texture = m_GameManager.Characters[(int)m_GameManager.Player2Char].CharacterDeadSprite;
                m_PlayerWonText.text = $"Player 1 won!";
            }
            else if (m_GameManager.Player1.Health <= 0)
            {

                m_WinningCharacter.texture = m_GameManager.Characters[(int)m_GameManager.Player2Char].CharacterSprites[m_GameManager.P2FinalHealth - 1];
                m_LosingCharacter.texture = m_GameManager.Characters[(int)m_GameManager.Player1Char].CharacterDeadSprite;
                m_PlayerWonText.text = $"Player 2 won!";
            }
        }
    }

    public void SelectCharacter(int _char)
    {
        if (Input.GetAxisRaw("Submit") != 0)
        {
            if (!player1Selected && !player2Selected)
            {
                m_GameManager.Player1Char = (GameManager.ECharacters)_char;
                Debug.Log($"Player 1 chose: {m_GameManager.Player1Char}");
                player1Selected = true;
            }
        }
        else if (Input.GetAxisRaw("Submit2") != 0)
        {
            if (player1Selected && !player2Selected)
            {
                m_GameManager.Player2Char = (GameManager.ECharacters)_char;
                Debug.Log($"Player 2 chose: {m_GameManager.Player2Char}");
                player2Selected = true;
            }
        }
    }
    

    public void LoadScene(int _sceneIndex)
    {
        StartCoroutine(Fadeing(_sceneIndex));
    }
    IEnumerator Fadeing(int _sceneIndex) 
    {
        if (activeScene == EScene.GameOver)
        {
            Destroy(m_GameManager.gameObject);
        }

        Time.timeScale = 0;

        m_Transitions.SetTrigger("Start");

        SceneManager.LoadScene(_sceneIndex);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
    }
    private IEnumerator Countdown()
    {
        Time.timeScale = 0;
        m_Countdown.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        m_Countdown.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        m_Countdown.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        m_Countdown.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        m_Countdown.text = "";
    }
}
