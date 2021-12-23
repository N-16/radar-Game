using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineSpawner : MonoBehaviour{
    [SerializeField] Radar radar;

    [SerializeField] float minD;
    [SerializeField] float maxD;
    [SerializeField] GameObject prefab;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float minSpeed, maxSpeed;

    void Start(){
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine(){
        while(true){
            AssignSubmarinePath(Instantiate(prefab, this.transform).GetComponent<Submarine>());
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        }
    }

    void AssignSubmarinePath(Submarine sub){
        float d = Random.Range(minD, maxD);
        Vector2 temp = Random.insideUnitCircle;
        Vector3 directionOfD = new Vector3(temp.x, 0f, temp.y).normalized;
        Vector3 pointOfContactOfDAndPath = radar.transform.position + (directionOfD * d);
        Vector3 spawnPos = pointOfContactOfDAndPath + (Vector3.Cross(directionOfD, Vector3.up) * radar.GetComponent<Radar>().radarZone);
        Vector3 deSpawnPos = pointOfContactOfDAndPath - (Vector3.Cross(directionOfD, Vector3.up) * (radar.GetComponent<Radar>().radarZone + 5f));
        sub.AssignPath(spawnPos, deSpawnPos, Random.Range(minSpeed, maxSpeed));
    }
}
