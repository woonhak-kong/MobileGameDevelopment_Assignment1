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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit :" + gameObject.name);
        Damaged(collision.GetComponent<Projectile>().GetPower());
    }

    private void Damaged(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            EnemySpawnManager.Instance.RemoveEnemyFromList(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
