using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }

    public void WeaponPowerUp()
    {
        weapon.PowerUp();
    }

    public void WeaponFireRateUp()
    {
        weapon.FireRateDown();
    }
}
