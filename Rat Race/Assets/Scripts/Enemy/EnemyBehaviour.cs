using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public bool isRaged = false;
    [SerializeField] private float rageDistance = 20;
    private NavMeshAgent agent;
    private Transform playerController;
    private float speed;
    [HideInInspector] public bool canAttack = true;
    private GameObject Player;
    [SerializeField] Image healthBar;
    [SerializeField] public float health;
    [SerializeField] ParticleSystem chainEffect;
    [SerializeField] public Transform arrowPos;
    public bool isActive = true;
    private float currentEnemyHealth;
    private GameObject EnemyDetecterScript;
    EnemyDetecter detecter;
    FightingZone zone;
    public int arrows = 0;
    private void Awake()
    {
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
        if (isActive == false)
        {
            agent.speed = 0;
            return;
        }
        else agent.speed = speed;
        float distance = Vector3.Distance(gameObject.transform.position, playerController.position);
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

    }
    IEnumerator Stunning(float delay)
    {
        chainEffect.Play();
        isActive = false;
        yield return new WaitForSeconds(delay);
        isActive = true;
    }
    public void GetDamage(float damage)
    {
        currentEnemyHealth -= damage;
        healthBar.fillAmount = currentEnemyHealth / health;
        //print(currentEnemyHealth);
        if (currentEnemyHealth <= 0)
        {
            zone.minusEnemy(gameObject);
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


}
