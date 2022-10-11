using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    //public GameObject[] EnemyPrefabs;
    public GameObject enemyParent;

    [SerializeField]
    private Transform[] SpawnPoint;
    [SerializeField]
    private float SpawnRate;
    [SerializeField]
    LinkedList<GameObject> EnemyList;

    private int orderInLayer = 0;

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
        //StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        int[] temp = new int[2] {-1,-1};
        while (LevelManager.Instance.IsStarting)
        {
            int randomRangeForSpawn = 0;
            yield return new WaitForSeconds(SpawnRate);
            do
            {
                randomRangeForSpawn = Random.Range(0, SpawnPoint.Length);

            }
            while (temp[0] == randomRangeForSpawn || temp[1] == randomRangeForSpawn);
            temp[0] = temp[1];
            temp[1] = randomRangeForSpawn;
            //print("WaitAndPrint " + Time.time);
            GameObject enemy = LevelManager.Instance.DequeueEnemyFromQueue();
            if (enemy != null)
            {
                //GameObject enemy = Instantiate(EnemyPrefabs[0], SpawnPoint[randomRangeForSpawn].position, EnemyPrefabs[0].transform.rotation);
                enemy.transform.position = SpawnPoint[randomRangeForSpawn].position;
                enemy.transform.rotation = enemy.transform.rotation;
                enemy.transform.SetParent(enemyParent.transform);
                enemy.SetActive(true);
                enemy.GetComponent<Renderer>().sortingOrder = orderInLayer++;
                EnemyList.AddLast(enemy);
                SortEnemyListByTransport();
            }
            else
            {

            }
            
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

    public void RemoveEnemyFromList(GameObject enemy)
    {
        EnemyList.Remove(enemy);
    }

    public int GetEnemyListCount()
    {
        return EnemyList.Count;
    }

    public LinkedList<GameObject> GetEnemyList()
    {
        return EnemyList;
    }


}
