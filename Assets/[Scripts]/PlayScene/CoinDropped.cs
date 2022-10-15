using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropped : MonoBehaviour
{
    private GameObject Coin;

    private int CoinValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        Coin = GameObject.Find("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, Coin.transform.position, 4.0f * Time.deltaTime);
        
    }

    public void SetCoinValue(int value)
    {
        CoinValue = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.AddCoin(CoinValue);
        SoundManager.Instance.Play("Coin");
        Destroy(this.gameObject);

    }
}
