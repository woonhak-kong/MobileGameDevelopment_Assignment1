using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float power;

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
        transform.Translate(Vector2.right * projectileSpeed * Time.fixedDeltaTime);
        if (transform.position.x > 30.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with " + collision.gameObject);
    }
}
