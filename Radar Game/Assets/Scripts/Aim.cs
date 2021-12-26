using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour{
    [SerializeField] Transform radarTransform;
    [SerializeField] float sensitivityMultiplier = 1000f;
    float sensi;
    public float yaw { get;private set;}

    void Awake(){
        sensi = PlayerPrefs.GetFloat("Sensitivity", 1f);
    }

    void Update(){
        yaw += Input.GetAxisRaw("Mouse X") * sensi * Time.deltaTime * sensitivityMultiplier;
        radarTransform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
