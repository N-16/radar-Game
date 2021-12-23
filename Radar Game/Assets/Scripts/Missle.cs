using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Missle : MonoBehaviour{
    [SerializeField] LayerMask ignoreRaycast;
    Vector3 velocity;
    Vector3 lastPosition;

    public static Action<Collider> MissleHitting;

    bool fired = false;

    public void Fire(Vector3 velocity, Vector3 startPos){
        fired = true;
        transform.position = startPos;
        transform.up = velocity;
        this.velocity = velocity;
        lastPosition = transform.position;
    }

    void Update(){
        if (fired){
            transform.position += velocity * Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - lastPosition, out hit, Vector3.Distance(lastPosition, transform.position), ~ignoreRaycast, QueryTriggerInteraction.Collide)){
                MissleHitting?.Invoke(hit.collider);
                Debug.Log("Colliding " + hit.collider.gameObject);
                fired = false;
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
