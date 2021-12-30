using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Submarine : MonoBehaviour{
    Vector3 moveFrom;
    Vector3 moveTo;
    Vector3 moveDirection;
    float shipSpeed;
    bool pathAssigned;
    public bool destroyed{private set; get;}
    [SerializeField] AudioSource explosionSfx;

    public static event Action OnSubKilled;
    public static event Action OnSubCrossed;

    void OnDisable(){
        GetComponent<Collider>().enabled = true;
        pathAssigned = false;
        destroyed = false;
    }
    void Start(){
        Missle.MissleHitting += CheckOnMissleHit;
    }
    void CheckOnMissleHit(Collider col){
        Debug.Log(gameObject);
        Debug.Log(col);
        if (col.gameObject == gameObject){
                explosionSfx.Play();
                destroyed = true;
                OnSubKilled?.Invoke();
                GetComponent<Collider>().enabled = false;
                StartCoroutine(AfterMissleHitRoutine());
            }
    }

    public void MissleHit(){
        explosionSfx.Play();
        destroyed = true;
        OnSubKilled?.Invoke();
        GetComponent<Collider>().enabled = false;
        StartCoroutine(AfterMissleHitRoutine());
    }

    public void AssignPath(Vector3 from, Vector3 to, float speed){
        moveFrom = from;
        moveTo = to;
        shipSpeed = speed;
        transform.position = from;
        pathAssigned = true;
    }
    void Update(){
        if (pathAssigned && !destroyed){
            transform.position += (moveTo - moveFrom).normalized *  shipSpeed * Time.deltaTime; 
            if (Vector3.Distance(transform.position , moveTo) < 0.2f){
                pathAssigned = false;
                OnSubCrossed?.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator AfterMissleHitRoutine(){
        while(explosionSfx.isPlaying){
            yield return null;
        }
        SoundManager.Instance.PlaySound(soundType.soundFx, "kill confirmed", () => this.gameObject.SetActive(false));
    }
}
