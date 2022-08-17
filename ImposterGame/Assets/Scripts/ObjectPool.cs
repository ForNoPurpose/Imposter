using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    [SerializeField] private int amountToPool = 20;
    [SerializeField] private GameObject poolObject;
    [SerializeField] private bool willGrow;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake() => SharedInstance = this;

    private void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject go = Instantiate(poolObject);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }

        if (willGrow)
        {
            GameObject go = Instantiate(poolObject);
            pool.Add(go);
            return go;
        }
        return null;
    }
}
