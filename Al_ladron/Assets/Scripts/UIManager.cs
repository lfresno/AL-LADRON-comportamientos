using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //In Game
    [SerializeField] private TMP_Text npcText;
    [SerializeField] private GameObject enemyFSM;
    private FSMenemy fsmBehaviour;
    private NPCState fsmState;

    void Awake(){
        Instance = this;
    }

    void Start() {
        fsmBehaviour = enemyFSM.GetComponent<FSMenemy>();
    }

    void Update() {
        npcText.SetText("FSM Enemy: " + fsmBehaviour.npcCurrent.ToString());
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
