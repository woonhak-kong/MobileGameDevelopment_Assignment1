using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private float Hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        SetHp(Hp);
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

    public void Damaged(float val)
    {
        Hp -= val;
        SetHp(Hp);
    }

    public void SetHp(float val)
    {
        Hp = val;
        float rate = Hp / 100;
        GameObject slider = GameObject.Find("HPSlider");
        slider.GetComponent<Slider>().value = rate;
    }


}
