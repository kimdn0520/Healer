using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    static ObjectPool objectPool;

    // Serializable Ű����� ����ڰ� ������ Ŭ���� �Ǵ� ����ü�� ������ �ν����Ϳ� ������ �� �ִ�.
    [Serializable]
    private class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
    // C# ������ List�� ���� �迭�̴�.
    [SerializeField] List<Pool> pools;                      // Pool���� �̸�, ������, ������ ������ ���ִ�.

    Dictionary<string, Queue<GameObject>> poolDictionary;   // Start���� Pool�� ������ ������ ���ӿ�����Ʈ�� �����ؼ� ��ųʸ��� �߰��Ѵ�.
    
    void Awake() => objectPool = this;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();   // ��ųʸ� �ʱ�ȭ

        // Pool�� ���� ������ ������ ���ӿ�����Ʈ�� �����Ͽ� ť�� �߰��Ѵ�.
        foreach(Pool pool in pools)
        {
            poolDictionary.Add(pool.tag, new Queue<GameObject>());

            for(int i = 0; i < pool.size; i++)
            {
                var obj = CreateNewObject(pool.tag, pool.prefab);

                poolDictionary[pool.tag].Enqueue(obj);
            }
        }
    }

    GameObject CreateNewObject(string tag, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);

        return obj;
    }

    public static GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        Queue<GameObject> poolQueue = objectPool.poolDictionary[tag];

        // ���� ť���� �ٻ̾ƴ� �Ἥ ������ ���� �ش�. 
        if (poolQueue.Count <= 0)
        {
            Pool pool = objectPool.pools.Find(x => x.tag == tag);

            var obj = objectPool.CreateNewObject(pool.tag, pool.prefab);
        }

        // ť���� ������ ���
        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    /// <summary>
    /// �ش��ϴ� ���ӿ�����Ʈ ��ũ��Ʈ�� OnDisable �Լ����� ���� ��Ű�� �ǰڴ�.
    /// </summary>
    /// <param name="obj"></param>
    public static void ReturnToPool(GameObject obj)
    {
        if (!objectPool.poolDictionary.ContainsKey(obj.name))
        {
            Debug.Log("poolDictionary�� ���°��� �����Ϸ��� �߽��ϴ�.");
            return;
        }

        objectPool.poolDictionary[obj.name].Enqueue(obj);
    }
}
