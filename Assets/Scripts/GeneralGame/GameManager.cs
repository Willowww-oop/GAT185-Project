using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int lives;

    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject winUI;


    enum eState
    {
        GAME,
        WIN,
        LOSE
    }

    eState state = eState.GAME;

    void Awake()
    {
        lives = 5;
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        livesText.text = "Lives: " + lives.ToString();

        switch (state)
        {
            case eState.GAME:
                

                if (lives < 1)
                {
                    state = eState.LOSE;
                }

                break;
            case eState.WIN:
                SetGameWon();

                break;
            case eState.LOSE:
                SetGameOver();

                break;
            default:
                break;
        }
    }

    public void SetGameWon()
    {
        winUI.SetActive(true);
    }

    public void SetGameOver()
    {
        gameoverUI.SetActive(true);
    }
}
