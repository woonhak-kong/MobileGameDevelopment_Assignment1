using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float power;

    private Vector2 direction = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * projectileSpeed * Time.fixedDeltaTime);
        if (transform.position.x > 30.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with " + collision.gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
