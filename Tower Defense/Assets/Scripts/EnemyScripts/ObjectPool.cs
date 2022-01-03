using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] [Range(1f, 50f)] int PoolSize = 5;
    [SerializeField][Range(0.1f, 10f)] float SpawnTimer = 1f;
    GameObject[] Pool;

    void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void PopulatePool()
    {
        Pool = new GameObject[PoolSize];
        for(int i =0; i < Pool.Length; i++)
        {
            Pool[i] = Instantiate(EnemyPrefab, transform);
            Pool[i].SetActive(false);
        }
    }
    void EnableObjectsInPool()
    {
        for( int i = 0; i < Pool.Length; i++)
        {
            if(Pool[i].activeInHierarchy == false)
            {
                Pool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(SpawnTimer);
        }
    }

}
