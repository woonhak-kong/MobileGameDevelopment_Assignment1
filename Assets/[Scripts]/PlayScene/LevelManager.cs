using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject[] EnemyPrefabs;
    public GameObject PreparedEnemies;

    public int Level { get; set; } = 1;

    public bool _isStarting = false;
    public bool IsStarting { get => _isStarting; set => _isStarting = value ; }

    private Queue<GameObject> enemyPreparedQueue = new Queue<GameObject>();

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
        //Invoke("StartLevel", 2.0f);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelSequence());
    }

    public void EndLevel()
    {

    }

    private void BuildEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefabs[0]);
            enemy.transform.SetParent(PreparedEnemies.transform);
            enemy.SetActive(false);
            enemyPreparedQueue.Enqueue(enemy);
        }
        //yield return null;
    }

    private IEnumerator StartLevelSequence()
    {
        yield return null;
        // build enemy
        BuildEnemies();
        // 5 second waiting animation

        yield return new WaitForSeconds(5.0f);
        // start level
        IsStarting = true;
        EnemySpawnManager.Instance.StartSpawnEnemy();
        StartCoroutine(TrackEnemyLeft());
        Debug.Log("end1");
        //yield return null;
    }

    private IEnumerator TrackEnemyLeft()
    {
        while(IsStarting)
        {
            if (enemyPreparedQueue.Count <= 0 && EnemySpawnManager.Instance.GetEnemyListCount() <= 0)
            {
                IsStarting = false;
            }
            yield return new WaitForSeconds(1.0f);
        }
        StartCoroutine(StartLevelSequence());
    }

    public GameObject DequeueEnemyFromQueue()
    {
        if (enemyPreparedQueue.Count > 0)
        {
            return enemyPreparedQueue.Dequeue();
        }
        return null;
    }
}
