using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour{
    [SerializeField] Transform radarTransform;
    float sensitivityMultiplier;
    float sensi;
    public float yaw { get;private set;}

    void Awake(){
        sensi = PlayerPrefs.GetFloat("Sensitivity", 4f);
    }

    void Update(){
        yaw += Input.GetAxisRaw("Mouse X") * sensi;
        radarTransform.rotation = Quaternion.Euler(0f, yaw, 0f);

    }
}
