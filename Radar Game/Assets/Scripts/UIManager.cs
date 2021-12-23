using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour{
    private static UIManager _instance;
    public static UIManager Instance{
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
        // for debugging purposes
        /*foreach(UIScreen screen in uiScreens){
            if (screen.screenName == "MainMenu"){
                activeScreen = screen;
            }
        }*/
    }
    [SerializeField] List<UIScreen> uiScreens = new List<UIScreen>();
    UIScreen activeScreen;
    public void ShowScreen(string screenName){
        if (activeScreen != null)
            activeScreen.uiScreen.SetActive(false);
        foreach(UIScreen screen in uiScreens){
            if (screen.screenName == screenName){
                screen.uiScreen.SetActive(true);
                activeScreen = screen;
                return;
            }
        }
        Debug.Log("UI screen not found");
    }

    public void RemoveAllScreen(){
        if (activeScreen != null)
        {
            activeScreen.uiScreen.SetActive(false);
            activeScreen = null;
        }
    }
}

[System.Serializable]
public class UIScreen
{
    [SerializeField] public string screenName;
    [SerializeField] public GameObject uiScreen;
}
