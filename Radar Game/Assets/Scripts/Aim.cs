using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour{
    [SerializeField] Transform radarTransform;
    [SerializeField] float sensi;
    float yaw;

    void Update(){
        yaw += Input.GetAxisRaw("Mouse X") * sensi;
        radarTransform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
