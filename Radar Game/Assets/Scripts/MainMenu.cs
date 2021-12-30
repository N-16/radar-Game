using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour{
    public void OnQuitButton(){
        UAP_AccessibilityManager.Say("Thank you for playing");
        Application.Quit();
    }

    public void OnInstructionButton(){
        UAP_AccessibilityManager.Say("Hear the detection notification sound after the radar pulse, and accordingly move the mouse horizontally to aim and press left click to shoot missle towards submarines. The later the notification sound is played after radar pulse, farther away the submarine is. If three submarines passes without being killed, the game ends. Total submarines destroyed will be the points you score. While in game press escape to pause it. Please use Headsets or a sound system with good stereo for better experience.");
    }

    public void OnPlayButton(){
        GameManager.Instance.StartGame();
    }
    public void OnAdjustSensitivityButton(){
        UIManager.Instance.ShowScreen("Adjust Sensitivity");
    }
}
