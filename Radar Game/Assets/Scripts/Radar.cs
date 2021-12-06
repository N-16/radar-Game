using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour{
    [SerializeField] float radarZone;
    [SerializeField] float pulseTime;
    [SerializeField] float pseudoSubRadius = 2f;
    [SerializeField] LayerMask submarineLayer;
    PseudoSubPool pool;
    Transform myTransform;

    void Awake(){
        pool = GetComponent<PseudoSubPool>();
        myTransform = transform;
    }
    void OnEnable(){
        StartCoroutine(PulseRoutine());
    }
    void OnDisable(){
        StopAllCoroutines();
    }
    IEnumerator PulseRoutine(){
        while(true){
            yield return new WaitForSeconds(pulseTime);
            Pulse();
        }
    }    

    void Pulse(){
        SoundManager.Instance.PlaySound(soundType.soundFx, "RadarWave");
        Collider[] subs = Physics.OverlapSphere(transform.position, radarZone, submarineLayer);
        int i = 0;
        foreach(Collider sub in subs){
            if (i + 1 < pool.pool.Count){
                pool.AddObject(5);
            }
            PseudoSub pseudoSub = pool.pool[i].GetComponent<PseudoSub>();
            pseudoSub.PlaySound(myTransform.position + (Vector3.Normalize(sub.transform.position - myTransform.position) * pseudoSubRadius), 
                Remap(0f, radarZone, Vector3.Distance(myTransform.position, sub.transform.position), pulseTime, 0f), pulseTime);
            i++;
        }
    }

    float Remap(float aMin, float aMax, float aValue, float bMax, float bMin){
        return Mathf.Lerp(bMin, bMax, Mathf.InverseLerp(aMin, aMax, aValue));
    }
}
