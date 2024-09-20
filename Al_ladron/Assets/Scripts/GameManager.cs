using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;
    private Player player;

    void Awake()  {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.MenuState);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if((player.life <=0) || Input.GetKeyDown(KeyCode.Escape)){
            UpdateGameState(GameState.PauseState);
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch(newState) {
            case GameState.MenuState:
                Time.timeScale = 0f;
                UIManager.Instance.MenuActive(true);
                UIManager.Instance.EndMenuActive(false);
                break;
            
            case GameState.GameState:
                Time.timeScale = 1.0f;
                player.ResetPlayer();
                UIManager.Instance.MenuActive(false);
                UIManager.Instance.EndMenuActive(false);
                break;

            case GameState.PauseState:
                Time.timeScale = 0f;
                UIManager.Instance.MenuActive(false);
                UIManager.Instance.EndMenuActive(true);
                break;

            default:
                break;
        }
    }
}

public enum GameState {
    MenuState,
    GameState,
    PauseState
}
