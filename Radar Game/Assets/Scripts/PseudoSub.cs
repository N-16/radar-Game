using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoSub : MonoBehaviour{

    float timeAfterSoundWillBePlayed;
    [SerializeField] AudioSource normalSource, highCutSource;
    void Awake(){

    }
    public void PlaySound(Vector3 psuedoSubPos, float normalSoundVolume, float highCutSoundVolume){
        transform.position = psuedoSubPos;
        /*StartCoroutine(PlaySoundRoutine(timeAfterSoundWillBePlayed, normalSoundVolume, highCutSoundVolume));
        StartCoroutine(StopSoundRoutine(timeAfterSoundShouldStop));*/
        normalSource.volume = normalSoundVolume;
        highCutSource.volume = highCutSoundVolume;
        normalSource.Play();
        highCutSource.Play();

    }
    /*IEnumerator PlaySoundRoutine(float time, float normalSoundVolume, float highCutSoundVolume){
        yield return new WaitForSeconds(time);
        normalSource.volume = normalSoundVolume;
        highCutSource.volume = highCutSoundVolume;
        normalSource.Play();
        highCutSource.Play();
    }
    IEnumerator StopSoundRoutine(float time){
        yield return new WaitForSeconds(time);
        normalSource.Stop();
    }*/
}
