using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] SpawnPoint;

    public GameObject[] EnemyPrefabs;

    [SerializeField]
    private float SpawnRate;
    public static EnemySpawnManager Instance { get; private set; }

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
            Instantiate(EnemyPrefabs[0], SpawnPoint[randomRangeForSpawn].position, EnemyPrefabs[0].transform.rotation);
        }
    }
    
}
