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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit :" + gameObject.name);
        Damaged(collision.GetComponent<Projectile>().GetPower());
    }

    private void Damaged(float damage)
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
        GameObject coin = Instantiate(LevelManager.Instance.CoinPrefab, LevelManager.Instance.CoinParent.transform);
        coin.transform.position = transform.position;
        coin.GetComponent<CoinDropped>().SetCoinValue(Coin);
        //LevelManager.Instance.AddCoin(Coin);
    }


    // call by animation event
    private void DestorySelf()
    {
        
        Destroy(this.gameObject);
    }
    
}
