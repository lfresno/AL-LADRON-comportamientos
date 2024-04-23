using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;

    //[SerializeField] private GameObject playerObj;
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
        if(player.life <=0){
            UpdateGameState(GameState.EndState);
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch(newState) {
            case GameState.MenuState:
                // SceneManager.LoadScene(0);
                UIManager.Instance.MenuActive(true);
                UIManager.Instance.EndMenuActive(false);
                break;
            
            case GameState.GameState:
                // SceneManager.LoadScene(1);
                player.ResetPlayer();
                UIManager.Instance.MenuActive(false);
                UIManager.Instance.EndMenuActive(false);
                break;

            case GameState.EndState:
                // SceneManager.LoadScene(2);
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
    EndState
}
