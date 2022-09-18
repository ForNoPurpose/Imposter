using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int poolSize;
}
public class ObjectPoolManager : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;
    public static ObjectPoolManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            //Destroy(this);
        }
    }

    public void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject pooledObject = Instantiate(pool.prefab);
                pooledObject.transform.parent = transform;
                pooledObject.SetActive(false);
                objectPool.Enqueue(pooledObject);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromObjectPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new ArgumentException($"Pool with tag {tag} doesn't exist");
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

