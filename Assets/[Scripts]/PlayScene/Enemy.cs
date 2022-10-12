using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{

    [SerializeField]
    protected float Speed;
    [SerializeField]
    protected float Hp;
    [SerializeField]
    protected float Power;
    [SerializeField]
    protected int Coin;
    [SerializeField]
    protected int Score;

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (this.transform.position.x < -10)
        {
            Vector2 direction = Player.transform.position - this.transform.position;
            direction.Normalize();
            transform.Translate(direction * Speed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * Speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit :" + gameObject.name);
        //Damaged(collision.GetComponent<Projectile>().GetPower());
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Damaged(Power);
            Dead();
        }
    }

    public void Damaged(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            
            Dead();
        }
    }

    private void Dead()
    {
        Speed = 0;
        EnemySpawnManager.Instance.RemoveEnemyFromList(this.gameObject);
        GetComponent<Animator>().SetBool("dead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        
        //LevelManager.Instance.AddCoin(Coin);
    }


    // call by animation event
    private void DestorySelf()
    {
        GameObject coin = Instantiate(LevelManager.Instance.CoinPrefab, LevelManager.Instance.CoinParent.transform);
        coin.transform.position = transform.position;
        coin.GetComponent<CoinDropped>().SetCoinValue(Coin);
        LevelManager.Instance.AddScore(Score);
        Destroy(this.gameObject);
    }
    
}
