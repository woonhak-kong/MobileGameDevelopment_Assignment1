using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    public GameObject[] EnemyPrefabs;
    public GameObject enemyParent;

    [SerializeField]
    private Transform[] SpawnPoint;
    [SerializeField]
    private float SpawnRate;
    [SerializeField]
    LinkedList<GameObject> EnemyList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyList = new LinkedList<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            int randomRangeForSpawn = Random.Range(0, SpawnPoint.Length);
            //print("WaitAndPrint " + Time.time);
            GameObject enemy = Instantiate(EnemyPrefabs[0], SpawnPoint[randomRangeForSpawn].position, EnemyPrefabs[0].transform.rotation);
            enemy.transform.SetParent(enemyParent.transform);
            EnemyList.AddLast(enemy);
            SortEnemyListByTransport();
        }
    }

    private void SortEnemyListByTransport()
    {
        float xPosition = float.MaxValue;
        GameObject leftmostEnemy = null;
        foreach(GameObject enemy in EnemyList)
        {
            if (enemy.transform.position.x < xPosition)
            {
                xPosition = enemy.transform.position.x;
                leftmostEnemy = enemy;
            }
        }

        if (leftmostEnemy != null)
        {
            EnemyList.Remove(leftmostEnemy);
            EnemyList.AddFirst(leftmostEnemy);
        }

    }

    public Vector3 GetLeftmostEnemyPosition()
    {
        return EnemyList.First.Value.transform.position;
    }
    
}
