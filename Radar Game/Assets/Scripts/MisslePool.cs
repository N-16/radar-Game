using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePool : MonoBehaviour{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize;
    [SerializeField] Transform misslesParent;

    List<GameObject> pool = new List<GameObject>();

    void Start(){
        for (int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(prefab, misslesParent);
            temp.SetActive(false);
            pool.Add(temp);
        }
    }
    public GameObject SpawnMissle(Vector3 pos)
    {
        foreach (GameObject missle in pool)
        {
            if (!missle.activeSelf)
            {
                missle.SetActive(true);
                missle.transform.position = pos;
                return missle;
            }
        }
        GameObject temp = Instantiate(prefab, misslesParent);
        pool.Add(temp);
        temp.SetActive(true);
        temp.transform.position = pos;
        return temp;
    }


}
