using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour{
    public void OnContinueButton(){
        GameManager.Instance.UnPause();
    }

    public void OnMainMenuButton(){
        GameManager.Instance.UnPause();
        GameManager.Instance.EndGame();
    }

    public void OnQuitButton(){
        Application.Quit();
    }
}
