using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleShooter : MonoBehaviour{
    [SerializeField] float missleSpeed;
    [SerializeField] float reloadTime;
    bool canShoot = true;
    MisslePool pool;

    void Start(){
        pool = GetComponent<MisslePool>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && GameManager.Instance.currentState != GameState.Pause){
            StartCoroutine(MissleShootRoutine());
        }
    }

    IEnumerator MissleShootRoutine(){
        canShoot = false;
        GameObject missle = pool.SpawnMissle(transform.position);
        missle.GetComponent<Missle>().Fire(transform.forward * missleSpeed, transform.position);
        SoundManager.Instance.PlaySound(soundType.soundFx, "MissleFire");
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    } 
}
