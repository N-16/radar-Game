using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Submarine : MonoBehaviour{
    Vector3 moveFrom;
    Vector3 moveTo;
    Vector3 moveDirection;
    float shipSpeed;
    bool pathAssigned;
    public bool destroyed{private set; get;}
    [SerializeField] AudioSource explosionSfx;

    void OnDisable(){
        pathAssigned = false;
        destroyed = false;
    }
    void Start(){
        Missle.MissleHitting += (Collider col) => {
            if (col.gameObject == gameObject){
                explosionSfx.Play();
                destroyed = true;
                StartCoroutine(AfterMissleHitRoutine());
            }
        };
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
                this.gameObject.SetActive(false);
                
            }
        }
    }

    IEnumerator AfterMissleHitRoutine(){
        while(explosionSfx.isPlaying){
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
