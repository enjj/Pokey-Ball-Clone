﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    #region Singleton

    public static ObjectPooler instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [System.Serializable]
    public class Pool {
        public Transform parentTransform;
        public string name;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public void InitializePool(string name, bool initialVisibility = false) {
        try {
            Pool pool = pools.Where(p => p.name == name).First();

            if (pool == null) {
                Debug.LogError("Pool with key " + name + " doesn't exists.");
                return;
            }

            if (poolDictionary.ContainsKey(pool.name)) {
                Debug.LogError("Pool with key " + pool.name + " already exists on dictionary.");
                return;
            }

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int ii = 0; ii < pool.size; ii++) {
                GameObject newObject = Instantiate(pool.prefab, pool.parentTransform);

                if (initialVisibility == false)
                    newObject.SetActive(false);

                objectPool.Enqueue(newObject);
            }

            poolDictionary.Add(pool.name, objectPool);

            Debug.Log("[OBJECT POOLER] " + pool.name + " pool has been initialized.");
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    public void ClearPool(string name) {
        Pool pool = pools.Where(p => p.name == name).First();

        if (pool == null) {
            Debug.LogError("Pool with key " + name + " doesn't exists.");
            return;
        }

        if (!poolDictionary.ContainsKey(pool.name)) {
            Debug.LogError("Pool with key " + pool.name + " didn't initiliaze yet.");
            return;
        }

        Queue<GameObject> willDeletePool = poolDictionary[pool.name];

        for (int ii = 0; ii < willDeletePool.Count; ii++) {
            Destroy(willDeletePool.Dequeue());
        }
    }

    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion? rotation = null) {
        if (!poolDictionary.ContainsKey(name)) {
            Debug.LogError("Pool with key " + name + " doesn't exists.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[name].Dequeue();
        if (rotation == null)
            objectToSpawn.transform.position = position;
        else
            objectToSpawn.transform.SetPositionAndRotation(position, (Quaternion)rotation);


        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null) {
            pooledObject.OnObjectReused();
        }

        poolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnFromPool(string name) {
        if (!poolDictionary.ContainsKey(name)) {
            Debug.LogError("Pool with key " + name + " doesn't exists.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[name].Dequeue();

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null) {
            pooledObject.OnObjectReused();
        }

        poolDictionary[name].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject[] GetGameObjectsOnPool(string poolName) {
        if (!poolDictionary.ContainsKey(poolName)) {
            Debug.LogError("Pool with key " + poolName + " doesn't exists.");
            return null;
        }

        GameObject[] objs = poolDictionary[poolName].ToArray();
        return objs;
    }

}