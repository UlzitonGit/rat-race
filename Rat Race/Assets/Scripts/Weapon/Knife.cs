using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Bullet")]
    public Transform spawnBullet;
    [SerializeField] private GameObject prefabBullet;
    public float speedBullet;
    public float timeLifeBullet;

    [Header("Hit")]
    [SerializeField] private float knifeDamage;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private LayerMask obstacles;

    [SerializeField] private Transform overlapPoint;
    
    [SerializeField] private float sphereRadius;

    [SerializeField] private bool _considerObstacles;
    
    
   

    [Header("Mana")]
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    public float HitMinusMana;

    [Header("Effects")]
    [SerializeField] GameObject trail;
    [SerializeField] Transform trailSpp;

    [Header("WeaponParent")]
    //[SerializeField] private GameObject meleTrigger;
    //[SerializeField] private Transform spp;
    [SerializeField] public float damage;
    [SerializeField] private Animator anim;

    public bool canAttack = true;

    
    
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
         manaUI = ManaUIScript.GetComponent<ManaUI>();
    }

    
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && canAttack == true)
        {
            AttackMele();
            OverlapAttack();
            StartCoroutine(VFX());
            
            manaUI.mana -= HitMinusMana;
        }
    }
    private void AttackMele()
    {
        if (canAttack == false) return;
        StartCoroutine(Attacking());
    }
   
    private void OverlapAttack()
    {
        Collider[] overlapEnemy = Physics.OverlapSphere(overlapPoint.position, sphereRadius, enemy);
        for(int i = 0; i < overlapEnemy.Length; i++)
        {      
          Vector3 Player = transform.position;
          Vector3 Enemy = overlapEnemy[i].transform.position;
          bool ObstaclesBeforEnemy = Physics.Linecast(Player, Enemy, obstacles.value);
            if (!ObstaclesBeforEnemy)
            {
                Debug.Log("SwordDamage");
                overlapEnemy[i].GetComponent<EnemyHealth>().GetDamage(knifeDamage);
            }            
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(overlapPoint.position, sphereRadius);       
    }
    IEnumerator Attacking()
    {
        canAttack = false;
        //GameObject mele = Instantiate(meleTrigger, spp.position, spp.rotation);
        //mele.GetComponent<WeaponTriger>().damage = damage;
        anim.SetTrigger("Attack");
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);
        //Destroy(mele);
        // yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attacking", false);
        canAttack = true;
    }
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject trailSpawned = Instantiate(trail, trailSpp);
        GameObject bullet = Instantiate(prefabBullet, spawnBullet.position, spawnBullet.rotation);
        bullet.GetComponent<WeaponTriger>().damage = damage;
        yield return new WaitForSeconds(0.2f);
        trailSpawned.transform.parent = null;
        yield return new WaitForSeconds(0.5f);
        Destroy(trailSpawned);

    }
}
