using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoSubPool : MonoBehaviour{
    [SerializeField] int poolSize;
    [SerializeField] GameObject prefab;
    [HideInInspector]public List<GameObject> pool = new List<GameObject>();
    [SerializeField] Transform psuedoSubParent;

    private void Awake(){
        AddObject(poolSize);
    }

    public GameObject EnableObject(){
        foreach(var obj in pool){
            if (!obj.activeSelf){
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject toAdd = Instantiate(prefab);
        toAdd.transform.parent = psuedoSubParent;
        toAdd.transform.localPosition = Vector3.zero;
        //toAdd.SetActive(false);
        pool.Add(toAdd);   
        return toAdd;
    }
    public void AddObject(int count){
        for (int i = 0; i < count; i++){
            GameObject toAdd = Instantiate(prefab, psuedoSubParent);
            toAdd.transform.localPosition = Vector3.zero;
            //toAdd.SetActive(false);
            pool.Add(toAdd);
        }
    }
}
