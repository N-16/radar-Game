using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{
    [SerializeField] List<GameScene> scenes = new List<GameScene>();
    private static SceneLoader _instance;
    public static SceneLoader Instance{
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
        Debug.unityLogger.logEnabled = false;
    }
    void Start(){
        AddScene(SceneType.UIScene, () => {
            UIManager.Instance.ShowScreen("MainMenu");
        });
    }

    public void AddScene(SceneType sceneType, Action onSceneLoaded = null){
        foreach(GameScene scene in scenes){
            if (scene.sceneType == sceneType){
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene.sceneName, LoadSceneMode.Additive);
                if (onSceneLoaded != null){
                    StartCoroutine(LoadSceneRoutine(loadOperation, onSceneLoaded));
                }
                return;
            }
        }
        Debug.Log("Scene Not found");
    }
    public void RemoveScene(SceneType sceneType){
        foreach(GameScene scene in scenes){
            if (scene.sceneType == sceneType){
                SceneManager.UnloadSceneAsync(scene.sceneName);
                return;
            }
        }
        Debug.Log("Scene Not found");
    }
    IEnumerator LoadSceneRoutine(AsyncOperation op, Action toDo){
        while(!op.isDone){
            yield return null;
        }
        toDo?.Invoke();
    }
}

[System.Serializable]
public class GameScene{
    [SerializeField] public string sceneName;
    [SerializeField] public SceneType sceneType;
}

public enum SceneType{
    GameScene, UIScene, GlobalScene, AdjustSensitivity
}
