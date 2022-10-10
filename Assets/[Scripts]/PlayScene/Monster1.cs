using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Enemy
{
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
        transform.Translate(Vector2.left * Speed * Time.fixedDeltaTime);
    }
}
