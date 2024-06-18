using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    //Main menu
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private Button startButton;

    //End menu
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Button restartButton, quitButton;

    void Awake(){
        Instance = this;
    }

    public void MenuActive( bool active) {
        menuCanvas.SetActive(active);
    }

    public void EndMenuActive( bool active) {
        endCanvas.SetActive(active);
    }


    public void StartPressed() {
        GameManager.Instance.UpdateGameState(GameState.GameState);
    }

    public void QuitPressed() {
        Application.Quit();
    }
}
