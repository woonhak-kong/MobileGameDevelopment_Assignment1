using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GameObject[] EnemyPrefabs;
    public GameObject PreparedEnemies;
    public GameObject CoinPrefab;
    public GameObject CoinParent;
    public GameObject LevelText;
    public GameObject PlayerPannel;

    public int Level { get; set; } = 1;

    public int Coin { get; set; } = 0;

    public int Score { get; set; } = 0;

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
        int numOfEnemies = 100 + (Level/5) * 2;
        int StrongestEnemyIdx = Level / 10;
        if (StrongestEnemyIdx > 2)
            StrongestEnemyIdx = 2;
        float ratioOfEnemy = Level < 10 ? 1.0f : (Level % 10) / 10.0f + 0.1f;
        for (int i = 0; i < numOfEnemies; i++)
        {
            float randNum = Random.Range(0.0f, 1.0f);
            // if randNum <= ratioOfEnemy => numOfEnemyType else numOfEnemyType -1
            int enemyIdx = randNum <= ratioOfEnemy ? StrongestEnemyIdx : StrongestEnemyIdx - 1;
            GameObject enemy = Instantiate(EnemyPrefabs[enemyIdx]);
            enemy.transform.SetParent(PreparedEnemies.transform);
            enemy.SetActive(false);
            enemyPreparedQueue.Enqueue(enemy);
        }
        //yield return null;
    }

    private IEnumerator StartLevelSequence()
    {
        yield return null;
        GameObject levelText = Instantiate(LevelText, PlayerPannel.transform);
        levelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + Level.ToString();
        //levelText.transform.parent = PlayerPannel.transform;
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

    private IEnumerator ClearAwards()
    {
        int numOfCoins = Level * 10;
        while(numOfCoins > 0)
        {
            GameObject coin = Instantiate(CoinPrefab, CoinParent.transform);
            coin.transform.position = new Vector2(Random.Range(-10.0f, 17.0f), -2.0f);
            numOfCoins--;
            coin.GetComponent<CoinDropped>().SetCoinValue(1);
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
        StartCoroutine(StartLevelSequence());
    }

    private IEnumerator TrackEnemyLeft()
    {
        while(IsStarting)
        {
            if (enemyPreparedQueue.Count <= 0 && EnemySpawnManager.Instance.GetEnemyListCount() <= 0)
            {
                ClearLevel();
            }
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    public GameObject DequeueEnemyFromQueue()
    {
        if (enemyPreparedQueue.Count > 0)
        {
            return enemyPreparedQueue.Dequeue();
        }
        return null;
    }

    private void ClearLevel()
    {
        IsStarting = false;
        Level++;
        EnemySpawnManager.Instance.StopAllCoroutines();
        StartCoroutine(ClearAwards());
        

    }

    public void AddCoin(int coin)
    {
        Coin += coin;
        FindObjectOfType<PlaySceneUIManager>().SetCoinText(this.Coin);
    }

    public void AddScore(int score)
    {
        Score += score;
        FindObjectOfType<PlaySceneUIManager>().SetScoreText(this.Score);
    }
}
