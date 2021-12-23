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
    }

    public GameState currentState {private set; get;}

    public void StartGame(){
        UIManager.Instance.RemoveAllScreen();
        SceneLoader.Instance.AddScene(SceneType.GameScene, () => {
            UAP_AccessibilityManager.Say("Start");
        });
    }
    public void EndGame(){
        SceneLoader.Instance.RemoveScene(SceneType.GameScene);
        UIManager.Instance.ShowScreen("MainMenu");
    }
}

public enum GameState{
    InGame, Menu, Pause
}
