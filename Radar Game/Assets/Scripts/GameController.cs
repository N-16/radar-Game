using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{
    [SerializeField] GameObject radar;
    [SerializeField] GameObject submarineSpawner;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject scoreScreen;
    int submarinePassedCount = 0;
    int submarineKilled = 0;
    int subSpawned;
    bool gameOver = false;
    void Start(){
        Submarine.OnSubKilled += () => {++submarineKilled; 
        };
        Submarine.OnSubCrossed += () => {++submarinePassedCount;
        if (submarinePassedCount == 1)
            SoundManager.Instance.PlaySound(soundType.soundFx, "1 sub crossed");
        else if (submarinePassedCount == 2)
            SoundManager.Instance.PlaySound(soundType.soundFx, "2 sub crossed");
        };
        SoundManager.Instance.PlaySound(soundType.ambience, "underwater");
        SoundManager.Instance.PlaySound(soundType.soundFx, "announce", () => {
            submarineSpawner.SetActive(true);
        });
    }
    void Update(){
        if (submarinePassedCount >= 3 && !gameOver){
            gameOver = true;
            radar.SetActive(false);
            submarineSpawner.SetActive(false);
            //UAP_AccessibilityManager.Say("Game Over. You scored " + submarineKilled.ToString() + " Points! Press Enter To return to main menu");
            scoreText.text = "Game Over. You scored " + submarineKilled.ToString() + " Points! Press escape to return to main menu";
            scoreScreen.SetActive(true);
            StartCoroutine(CheckForEnterRoutine());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver && GameManager.Instance.currentState == GameState.InGame){
            GameManager.Instance.PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.currentState == GameState.Pause){
            GameManager.Instance.UnPause();
        }
    }
    IEnumerator CheckForEnterRoutine(){
        while (!Input.GetKey(KeyCode.Escape)){
            yield return null;
        }
        GameManager.Instance.EndGame();
    }
}
