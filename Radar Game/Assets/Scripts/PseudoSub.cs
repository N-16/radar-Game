using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoSub : MonoBehaviour{

    float timeAfterSoundWillBePlayed;
    AudioSource source;
    void Awake(){
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(Vector3 psuedoSubPos, float timeAfterSoundWillBePlayed, float timeAfterSoundShouldStop){
        transform.position = psuedoSubPos;
        StartCoroutine(PlaySoundRoutine(timeAfterSoundWillBePlayed));
        StartCoroutine(StopSoundRoutine(timeAfterSoundShouldStop));

    }
    IEnumerator PlaySoundRoutine(float time){
        yield return new WaitForSeconds(time);
        source.Play();
    }
    IEnumerator StopSoundRoutine(float time){
        yield return new WaitForSeconds(time);
        source.Stop();
    }    
}
