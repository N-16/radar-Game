using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour{
    public void OnQuitButton(){
        UAP_AccessibilityManager.Say("Thank you for playing");
        Application.Quit();
    }

    public void OnInstructionButton(){
        UAP_AccessibilityManager.Say("Hear the detection notification sound, and accordingly move the mouse horizontally to aim and shoot missle towards submarines. Press P or escape to pause the game.");
    }

    public void OnPlayButton(){
        GameManager.Instance.StartGame();
    }
    public void OnAdjustSensitivityButton(){
        UIManager.Instance.ShowScreen("Adjust Sensitivity");
    }
}
