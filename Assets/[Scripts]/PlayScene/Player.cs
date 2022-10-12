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

    Bounds bound;
    private bool isMouseHolding = false;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        SetHp(Hp);
        bound.SetMinMax(new Vector2(-20.0f, -6.0f), new Vector2(-15.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        Vector2 directionToMousePoint;
#if UNITY_EDITOR
        // key
        float yAxis = Input.GetAxis("Vertical");
        float yPosition = transform.position.y + 10 * yAxis * Time.deltaTime;
        
        if (yPosition < bound.min.y || yPosition > bound.max.y)
        {
            yPosition = transform.position.y;
        }
        transform.position = new Vector2(transform.position.x, yPosition);

        // mouse
        if (Input.GetMouseButtonDown(0))
            isMouseHolding = true;
        if (Input.GetMouseButtonUp(0))
            isMouseHolding = false;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isMouseHolding && worldPosition.x > -13.0f)
        {
            directionToMousePoint = worldPosition - weapon.GetMuzzlePosition().position;
            directionToMousePoint.Normalize();
            weapon.SetWeaponRotation(directionToMousePoint);
        }
        else
        {
            weapon.SetWeaponRotation(Vector2.right);
        }


#endif

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
