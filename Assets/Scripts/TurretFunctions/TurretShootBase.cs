using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootBase : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Transform muzzle;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static TurretShootBase instance;

    public virtual void Shoot(GameObject go)
    {
        TurretShootBase.instance.SpawnFromPool("Cube", muzzle.transform.position, muzzle.rotation);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Tag does not exist");
            return null;
        }

        GameObject objecttospawn = poolDictionary[tag].Dequeue();
        objecttospawn.SetActive(true);
        objecttospawn.transform.position = position;
        objecttospawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objecttospawn);
        return objecttospawn;
        
    }


}
