using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

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
        weapon.SetWeaponRotation(Vector2.right);
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
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    break;
                }

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z));

                    if (touchPosition.x <= - 13.0f)
                    {
                        Vector2 newPosition = touchPosition;
                        if (touchPosition.y > bound.max.y)
                        {
                            newPosition.y = bound.max.y;
                        }
                        if (touchPosition.y < bound.min.y)
                        {
                            newPosition.y = bound.min.y;
                        }
                        transform.position = new Vector2(transform.position.x, newPosition.y);
                    }
                    else
                    {
                        directionToMousePoint = touchPosition - weapon.GetMuzzlePosition().position;
                        directionToMousePoint.Normalize();
                        weapon.SetWeaponRotation(directionToMousePoint);

                    }

                }
            }
            
        }



#endif


        if (Hp <= 0)
        {
            GameOver();
        }


    }

    private void GameOver()
    {
        FindObjectOfType<PlaySceneUIManager>().GameOver();
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
