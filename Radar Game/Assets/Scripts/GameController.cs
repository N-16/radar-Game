using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
    [SerializeField] GameObject radar;
    [SerializeField] GameObject submarineSpawner;
    int submarinePassedCount = 0;
    int submarineKilled = 0;
    bool gameOver = false;
    void Start(){
        Submarine.OnSubKilled += () => ++submarineKilled;
        Submarine.OnSubCrossed += () => {++submarinePassedCount;
        if (submarinePassedCount < 3)
            UAP_AccessibilityManager.Say("Warning, " + submarinePassedCount.ToString() + " submarine crossed");
        };
    }
    void Update(){
        if (submarinePassedCount >= 3 && !gameOver){
            gameOver = true;
            radar.SetActive(false);
            submarineSpawner.SetActive(false);
            UAP_AccessibilityManager.Say("Game Over. You scored " + submarineKilled.ToString() + " Points! Press Enter To return to main menu");
            StartCoroutine(CheckForEnterRoutine());
        }
    }
    IEnumerator CheckForEnterRoutine(){
        while (!Input.GetKey(KeyCode.Return)){
            yield return null;
        }
        GameManager.Instance.EndGame();
    }
}
