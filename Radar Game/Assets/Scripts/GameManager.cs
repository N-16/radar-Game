using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    private static GameManager _instance;
    public static GameManager Instance{
        get{
            if (_instance == null){
                Debug.LogError("NULL INSTANCE!");
                return null;
            }
            return _instance;
        }
    }
    void Awake(){
        _instance = this;
        currentState = GameState.Menu;
    }
    void Update(){
        /*if (Input.GetKeyDown(pauseKey) && currentState == GameState.InGame){
            currentState = GameState.Pause;
            Time.timeScale = 0f;
            UAP_AccessibilityManager.Say("Game Paused");
            UIManager.Instance.ShowScreen("Pause");
            return;
        }
        else if (Input.GetKeyDown(pauseKey) && currentState == GameState.Pause){
            Debug.Log("Should be unpaused");
            Time.timeScale = 1f;
            currentState = GameState.InGame;
            UIManager.Instance.RemoveAllScreen();
        }*/
    }

    public GameState currentState {private set; get;}
    [SerializeField] KeyCode pauseKey;

    public void StartGame(){
        UIManager.Instance.RemoveAllScreen();
        SceneLoader.Instance.AddScene(SceneType.GameScene, () => {
            UAP_AccessibilityManager.Say("Start");
            currentState = GameState.InGame;
        });
    }
    public void EndGame(){
        SceneLoader.Instance.RemoveScene(SceneType.GameScene);
        UIManager.Instance.ShowScreen("MainMenu");
        currentState = GameState.Menu;
    }
    public void UnPause(){
        if (currentState == GameState.Pause){
            Time.timeScale = 1f;
            currentState = GameState.InGame;
            UIManager.Instance.RemoveAllScreen();
        }
    }
    public void PauseGame(){
        if (currentState == GameState.InGame){
            currentState = GameState.Pause;
            Time.timeScale = 0f;
            UAP_AccessibilityManager.Say("Game Paused");
            UIManager.Instance.ShowScreen("Pause");
            return;
        }
    }
    
}

public enum GameState{
    InGame, Menu, Pause
}
