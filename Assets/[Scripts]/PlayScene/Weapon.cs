using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectilePrefab;
    public GameObject bulletsParrent;

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
            GameObject bullet = Instantiate(projectilePrefab, muzzlePosition.position, projectilePrefab.transform.rotation);
            bullet.transform.SetParent(bulletsParrent.transform);
            Vector2 direction = EnemySpawnManager.Instance.GetLeftmostEnemyPosition() - bullet.transform.position;
            direction.Normalize();
            bullet.GetComponent<Projectile>().SetDirection(direction);


        }
    }
}
