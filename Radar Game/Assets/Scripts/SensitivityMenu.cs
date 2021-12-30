using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityMenu : MonoBehaviour{
    [SerializeField] Slider slider;

    void Awake(){
        slider.value = PlayerPrefs.GetFloat("Sensitivity", 4f);
    }
    public void OnSensiChange(Slider slider){
        PlayerPrefs.SetFloat("Sensitivity", slider.value);
    }

    public void OnCheckSensitivity(){
        SceneLoader.Instance.AddScene(SceneType.AdjustSensitivity, () => {
            UAP_AccessibilityManager.Say("Move the mouse horizontally to feel sensitivity. A sound will be played for rotation of every 90 degrees. Press escape to go back and adjust sensitivity");
            UIManager.Instance.RemoveAllScreen();
        });
    }
    public void OnGobackButton(){
        UIManager.Instance.ShowScreen("MainMenu");
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            OnGobackButton();
        }
    }
}
