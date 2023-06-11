using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    static ObjectPool objectPool;

    // Serializable 키워드로 사용자가 정의한 클래스 또는 구조체의 정보를 인스펙터에 노출할 수 있다.
    [Serializable]
    private class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
    // C# 에서는 List가 동적 배열이다.
    [SerializeField] List<Pool> pools;                      // Pool에는 이름, 프리팹, 사이즈 정보가 들어가있다.

    Dictionary<string, Queue<GameObject>> poolDictionary;   // Start에서 Pool의 정보를 가지고 게임오브젝트를 생성해서 딕셔너리에 추가한다.
    
    void Awake() => objectPool = this;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();   // 딕셔너리 초기화

        // Pool에 넣은 정보를 가지고 게임오브젝트를 생성하여 큐에 추가한다.
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

        // 만약 큐에서 다뽑아다 써서 없으면 만들어서 준다. 
        if (poolQueue.Count <= 0)
        {
            Pool pool = objectPool.pools.Find(x => x.tag == tag);

            var obj = objectPool.CreateNewObject(pool.tag, pool.prefab);
        }

        // 큐에서 꺼내서 사용
        GameObject objectToSpawn = poolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    /// <summary>
    /// 해당하는 게임오브젝트 스크립트의 OnDisable 함수에서 실행 시키면 되겠다.
    /// </summary>
    /// <param name="obj"></param>
    public static void ReturnToPool(GameObject obj)
    {
        if (!objectPool.poolDictionary.ContainsKey(obj.name))
        {
            Debug.Log("poolDictionary에 없는것을 리턴하려고 했습니다.");
            return;
        }

        objectPool.poolDictionary[obj.name].Enqueue(obj);
    }
}
