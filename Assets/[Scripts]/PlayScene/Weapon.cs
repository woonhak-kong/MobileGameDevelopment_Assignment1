using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectilePrefab;
    [SerializeField]
    private Transform muzzlePosition;
    [SerializeField]
    private float shootingDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingDelay);
            //print("WaitAndPrint " + Time.time);
            Instantiate(projectilePrefab, muzzlePosition.position, projectilePrefab.transform.rotation);
        }
    }
}
