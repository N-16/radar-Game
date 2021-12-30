using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Missle : MonoBehaviour{
    [SerializeField] LayerMask ignoreRaycast;
    [SerializeField] LayerMask submarineLayer;
    Vector3 velocity;
    Vector3 lastPosition;

    public static Action<Collider> MissleHitting;

    bool fired = false;
    float timeAtFire;

    public void Fire(Vector3 velocity, Vector3 startPos){
        fired = true;
        transform.position = startPos;
        transform.up = velocity;
        this.velocity = velocity;
        lastPosition = transform.position;
        timeAtFire = Time.time;
    }

    void Update(){
        if (fired){
            if (Time.time - timeAtFire > 10f){
                gameObject.SetActive(false);
                return;
            }
            transform.position += velocity * Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - lastPosition, out hit, Vector3.Distance(lastPosition, transform.position), submarineLayer, QueryTriggerInteraction.Collide)){
                Debug.Log("Colliding " + hit.collider.gameObject);
                //MissleHitting?.Invoke(hit.collider);
                hit.collider.gameObject.GetComponent<Submarine>().MissleHit();
                fired = false;
                gameObject.SetActive(false);
            }
            lastPosition = transform.position;
        }
    }
    void OnDisable(){
        fired = false;
        velocity = Vector3.zero;
        lastPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
