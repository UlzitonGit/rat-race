using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [Header("Fly")]
    [SerializeField] private float duration;
    [SerializeField] private float durationFall;
    [SerializeField] private float timeExplosion;
    [SerializeField] private float speedBullet;
    [SerializeField] private Vector3 dir;
    [Header("Explosion")]
    [SerializeField] private float damageExplosion;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private GameObject explosionVFX;

    private Transform overlapPoint;

    [SerializeField] private float sphereRadius;
    private Rigidbody rb;
    private bool flying;

    [Header("Mana")]
    public float damagePlayerExplosion;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Fly());
        overlapPoint = GetComponent<Transform>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
    }
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (flying)
        {
            transform.Translate(dir * speedBullet * Time.deltaTime);

        }
       

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Explosion();
        }
    }
    private void Explosion()
    {
        Collider[] overlapEnemy = Physics.OverlapSphere(overlapPoint.position, sphereRadius, enemy);
        for (int i = 0; i < overlapEnemy.Length; i++)
        {
            Vector3 Bullet = transform.position;
            Vector3 Enemy = overlapEnemy[i].transform.position;
            bool ObstaclesBeforEnemy = Physics.Linecast(Bullet, Enemy, obstacles.value);
            if (!ObstaclesBeforEnemy)
            {
                
                overlapEnemy[i].GetComponent<EnemyHealth>().GetDamage(damageExplosion);
                
            }
        }
        Collider[] overlapPlayer = Physics.OverlapSphere(overlapPoint.position, sphereRadius, player);
        for (int i = 0; i < overlapPlayer.Length; i++)
        {
            Vector3 Bullet = transform.position;
            Vector3 Player = overlapPlayer[i].transform.position;
            bool ObstaclesBeforEnemy = Physics.Linecast(Bullet, Player, obstacles.value);
            if (!ObstaclesBeforEnemy)
            {
                manaUI.mana -= damagePlayerExplosion;
            }
        }
        Instantiate(explosionVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    IEnumerator Fly()
    {
        flying = true;
        yield return new WaitForSecondsRealtime(durationFall);
        rb.useGravity = true;
        yield return new WaitForSecondsRealtime(duration);
        flying = false;
        yield return new WaitForSecondsRealtime(timeExplosion);
        Explosion();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(overlapPoint.position, sphereRadius);
    }
}
