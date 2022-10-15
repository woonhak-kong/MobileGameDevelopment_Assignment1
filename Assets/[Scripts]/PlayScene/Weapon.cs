using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectilePrefab;
    public GameObject gunExplosionPrefab;
    public GameObject bulletsParrent;

    [SerializeField]
    private Transform muzzlePosition;
    [SerializeField]
    private float shootingDelay;
    [SerializeField]
    private float power;

    
    private Vector2 direction;

    private LineRenderer lr;

    private Bounds Laserbound;
    

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        StartCoroutine(Shoot());
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        Laserbound.SetMinMax(new Vector2(-22.0f, -8.0f), new Vector2(21.0f, 3.5f));

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject myLine = new GameObject();
        //myLine.transform.position = start;

        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        
        lr.SetPosition(0, muzzlePosition.position);
        Vector2 positionVec2 = muzzlePosition.position;
        Vector2 endPosition = positionVec2 + direction * 30.0f;
        lr.SetPosition(1, endPosition);
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

            //Vector2 direction = EnemySpawnManager.Instance.GetLeftmostEnemyPosition() - muzzlePosition.transform.position;
            //direction.Normalize();

            //// rotating weapon and shooting
            //float angle = Vector2.SignedAngle(transform.right, direction);
            ////Debug.Log("weapon " + angle);
            //transform.Rotate(Vector3.forward, angle);

            GameObject bullet = Instantiate(projectilePrefab, muzzlePosition.position, muzzlePosition.rotation);
            bullet.transform.SetParent(bulletsParrent.transform);
            bullet.GetComponent<Projectile>().SetPower(power);

            GameObject explo = Instantiate(gunExplosionPrefab);
            explo.transform.position = muzzlePosition.position;

            SoundManager.Instance.Play("Shoot");
        }
    }

    public void FireRateDown()
    {
        
        shootingDelay -= 0.002f;
    }

    public void SetPower(float power)
    {
        this.power = power;
    }

    public void PowerUp()
    {
        power += 0.02f;
    }

    public Transform GetMuzzlePosition()
    {
        return muzzlePosition;
    }

    public void SetWeaponRotation(Vector2 direction)
    {
        // rotating weapon
        this.direction = direction;
        float angle = Vector2.SignedAngle(transform.right, this.direction);
        transform.Rotate(Vector3.forward, angle);
    }
}
