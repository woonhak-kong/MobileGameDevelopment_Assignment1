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
            if (EnemySpawnManager.Instance.GetEnemyListCount() <= 0)
            {
                continue;
            }

            Vector2 direction = EnemySpawnManager.Instance.GetLeftmostEnemyPosition() - muzzlePosition.transform.position;
            direction.Normalize();

            // rotating weapon and shooting
            float angle = Vector2.SignedAngle(transform.right, direction);
            //Debug.Log("weapon " + angle);
            transform.Rotate(Vector3.forward, angle);

            GameObject bullet = Instantiate(projectilePrefab, muzzlePosition.position, muzzlePosition.rotation);
            bullet.transform.SetParent(bulletsParrent.transform);
        }
    }
}
