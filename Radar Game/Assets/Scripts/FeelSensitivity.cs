using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelSensitivity : MonoBehaviour{
    AudioSource audioSource;
    Threshold lastThreshold;
    float lowerThresh, upperThresh;
    int soundPitch;
    Aim aim;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
        aim = GetComponent<Aim>();
    }
    void Start(){
        lowerThresh = aim.yaw - 1f;
        upperThresh = aim.yaw + 89f;
        soundPitch = 2;
        lastThreshold = Threshold.Upper;
        UAP_AccessibilityManager.Say("Move the mouse horizontally to feel sensitivity. A sound will be played for rotation of every 90 degrees. Press escape to go back and adjust sensitivity");
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            UIManager.Instance.ShowScreen("Adjust Sensitivity");
            SceneLoader.Instance.RemoveScene(SceneType.AdjustSensitivity);
        }
    }

    void LateUpdate(){
        if (aim.yaw < lowerThresh){
            upperThresh = lowerThresh;
            lowerThresh = upperThresh - 90f;
            if (lastThreshold == Threshold.Upper){
                audioSource.Play();
            }
            else{
                if (soundPitch == 2){
                    Debug.Log("pitch should be 0.8f");
                    soundPitch = 8;
                }
                else{
                    soundPitch -= 2;
                }
                audioSource.pitch = soundPitch * 0.1f;
                audioSource.Play();
            }
        lastThreshold = Threshold.Lower;
            return;
        }
        if (aim.yaw > upperThresh){
            lowerThresh = upperThresh;
            upperThresh = lowerThresh + 90f;            
            if (lastThreshold == Threshold.Lower){
                audioSource.Play();
            }
            else{
                if (soundPitch == 8){
                    soundPitch = 2;
                }
                else{
                    soundPitch += 2;
                }
                audioSource.pitch = soundPitch * 0.1f;
                audioSource.Play();
            }
        lastThreshold = Threshold.Upper;
        }
        
    }
    public enum Threshold{
        Upper, Lower
    }

}
