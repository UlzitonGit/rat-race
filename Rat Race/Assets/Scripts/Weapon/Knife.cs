using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Bullet")]
    public Transform spawnBullet;
    [SerializeField] private GameObject prefabBullet;
    public float speedBullet;
    

    [Header("Hit")]
    [SerializeField] private float knifeDamage;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float durationBetweenAttacks = 0.3f;
    [SerializeField] private Transform overlapPoint;
    
    [SerializeField] private float sphereRadius;

    
    
    
   

    [Header("Mana")]
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    public float HitMinusMana;

    [Header("Effects")]
    [SerializeField] GameObject trail;
    [SerializeField] Transform trailSpp;
    //[SerializeField] private GameObject meleTrigger;
    //[SerializeField] private Transform spp;
    
    [SerializeField] private Animator anim;

    [HideInInspector] public bool canAttack = true;

    [Header("Ability")]

    [SerializeField] private float KD1Ability;
    [SerializeField] private float KD2Ability;
    [SerializeField] private float KD3Ability;
    [SerializeField] private float minusMana1Ability;
    [SerializeField] private float minusMana2Ability;
    [SerializeField] private float minusMana3Ability;
    
    

    private EnemyDetecter enemyDetecter;
    private PlayerController playerController;
    private GameObject NearEnemyGameOb;
    private Transform NearEnemyPoint;

    private bool can1Ablty = true;
    private bool can2Ablty = true;
    private bool can3Ablty = true;
    private bool is3Ablty = false;
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
         manaUI = ManaUIScript.GetComponent<ManaUI>();
        enemyDetecter = GetComponentInChildren<EnemyDetecter>();
        playerController = GetComponent<PlayerController>();
        
    }

    
    void FixedUpdate()
    {
        
        KnifeAbility1();
        KnifeAbility2();
        KnifeAbility3();
        if (Input.GetKey(KeyCode.Mouse0) && canAttack)
        {
            AttackMele();
            OverlapAttack();
            StartCoroutine(VFX());
            
            manaUI.mana -= HitMinusMana;
        }
    }
    private void AttackMele()
    {
        if (!canAttack)
        {
            return;
        }
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
                overlapEnemy[i].GetComponent<EnemyBehaviour>().GetDamage(knifeDamage);
            }            
        }
    }
    private void KnifeAbility1()
    {
        if (Input.GetKey(KeyCode.Z) && playerController.concentrate && can1Ablty)
        {
            playerController.characterController.enabled = false;
            NearEnemyGameOb = enemyDetecter.enemies[playerController.enemyToConcentrate];
            NearEnemyPoint = NearEnemyGameOb.transform.Find("1KnifeAbility");
            transform.position = NearEnemyPoint.position;
            manaUI.mana -= minusMana1Ability;
            playerController.characterController.enabled = true;
            StartCoroutine(FirstAbility());
        }
    }
    private void KnifeAbility2()
    {
        if (Input.GetKey(KeyCode.X) && playerController.concentrate && can2Ablty)
        {
            manaUI.mana -= minusMana2Ability;
            enemyDetecter.enemies[playerController.enemyToConcentrate].GetComponent<EnemyBehaviour>().Mark();
            StartCoroutine(SecondAbility());
        }
    }
    private void KnifeAbility3()
    {
        if (Input.GetKey(KeyCode.C) && playerController.concentrate && can3Ablty)
        {
            manaUI.mana -= minusMana3Ability;
            is3Ablty = true;
            StartCoroutine(ThirdAbility());
            
        }
       
    }

    IEnumerator FirstAbility()
    {
        can1Ablty = false;
        yield return new WaitForSeconds(KD1Ability);
        can1Ablty = true;
    }
    IEnumerator SecondAbility()
    {
        can2Ablty = false;
        yield return new WaitForSeconds(KD2Ability);
        can2Ablty = true;
    }
    IEnumerator ThirdAbility()
    {
        can3Ablty = false;
        yield return new WaitForSeconds(KD3Ability);
        can3Ablty = true;
    }
    IEnumerator Attacking()
    {
        canAttack = false;
        //GameObject mele = Instantiate(meleTrigger, spp.position, spp.rotation);
        //mele.GetComponent<WeaponTriger>().damage = damage;
        playerController.AttackDash();
        anim.SetTrigger("Attack");
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(durationBetweenAttacks);
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
        //bullet.GetComponent<WeaponTriger>().damage = damage;
        yield return new WaitForSeconds(0.2f);
        trailSpawned.transform.parent = null;
        yield return new WaitForSeconds(0.5f);
        Destroy(trailSpawned);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(overlapPoint.position, sphereRadius);       
    }
}
