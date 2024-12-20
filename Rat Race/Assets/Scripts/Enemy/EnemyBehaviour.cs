using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public bool isRaged = false;
    [SerializeField] private float rageDistance = 20;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform playerController;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public float speed;
    [HideInInspector] public bool canAttack = true;
    [HideInInspector] public float distance;
    [SerializeField] ParticleSystem markeEffect;
    [SerializeField] public float health;
    [SerializeField] ParticleSystem chainEffect;
    [SerializeField] private LayerMask enemy;
   
    [SerializeField] public Transform arrowPos;
    [SerializeField] private ParticleSystem secondBowPart;
    [SerializeField] private ParticleSystem secondBowPartMark;
    [SerializeField] private GameObject secondBow;
    [HideInInspector] public Rigidbody rb;
    public bool isActive = true;
    public float currentEnemyHealth;
    private GameObject EnemyDetecterScript;
    EnemyDetecter detecter;
    FightingZone zone;
    public int arrows = 0;
    private float damageMark = 0;
    private bool isMarked = false;
    private bool bowSecond = false;
    private GameObject bowSecondDamage;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player");
        playerController = Player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        EnemyDetecterScript = GameObject.FindWithTag("Detecter");
        currentEnemyHealth = health;
        detecter = EnemyDetecterScript.GetComponent<EnemyDetecter>();
    }
    // Start is called before the first frame update

    // Update is called once per frame

    public void SearchForPlayer()
    {
        if (!isActive)
        {
            agent.speed = 0;
            return;
        }
        else agent.speed = speed;



        distance = Vector3.Distance(gameObject.transform.position, playerController.position);



        if (distance < rageDistance)
        {
            agent.SetDestination(playerController.position);
            agent.speed = speed;
            isRaged = true;

        }
        else
        {
            isRaged = false;
            agent.speed = 0;
        }
    }
    public void StunnByArrow()
    {
        if (arrows > 4) arrows = 4;
        if(arrows == 0) arrows = 1;
        StartCoroutine(Stunning(arrows * 0.75f));
        foreach (Transform child in arrowPos.transform)
        {
            Destroy(child.gameObject);
        }

    }
    IEnumerator Stunning(float delay)
    {
        chainEffect.Play();
        isActive = false;
        yield return new WaitForSeconds(delay);
        isActive = true;
    }
    IEnumerator Discarding()
    {
        agent.enabled = false;
        isActive = false;
        rb.AddForce(transform.forward * -10f, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        agent.enabled = true;
        isActive = true;
    }
    IEnumerator Marking()
    {
        if(isMarked == false)
        {
            isMarked = true;
            damageMark = 0;
            markeEffect.Play();
            yield return new WaitForSeconds(4);
            print(damageMark);
            GetDamage(damageMark);
            isMarked = false;
            damageMark = 0;
        }
       
    }
    IEnumerator DamageOther()
    {
        if (bowSecond == false)
        {
            secondBowPartMark.Play();
            print("yes");
            bowSecond = true;           
            yield return new WaitForSeconds(6);           
            bowSecond = false;
           
        }

    }
    private void OverlapAttack(float damage)
    {
        print(damage);
        secondBowPart.Play();
        bowSecondDamage= Instantiate(secondBow, transform.position, Quaternion.identity);
        bowSecondDamage.GetComponent<WeaponTriger>().damage = damage;
        bowSecondDamage.GetComponent<WeaponTriger>().parent = gameObject;

        
    }
    public virtual void GetDamage(float damage)
    {
        currentEnemyHealth -= damage;
        if (bowSecond == true) OverlapAttack(damage);
        if (isMarked == true) damageMark += damage;
        if (currentEnemyHealth <= 0)
        {
            //zone.minusEnemy(gameObject);
            detecter.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FightZone"))
        {
            zone = other.GetComponent<FightingZone>();
            zone.PlusEnemy(gameObject);
        }
    }
    public void Discard()
    {
        StartCoroutine(Discarding());
    }
    public void Mark()
    {
        StartCoroutine(Marking());
    }
    public void BowSecond()
    {
        StartCoroutine(DamageOther());
    }
}
