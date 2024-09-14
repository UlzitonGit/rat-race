using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public bool isRaged = false;
    [SerializeField] private float rageDistance = 20;
    NavMeshAgent agent;
    private Transform playerController;
    private float speed;
    public bool canAttack = true;
    private GameObject Player;
    
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        playerController = Player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
    }
    // Start is called before the first frame update

    // Update is called once per frame
   
    public void SearchForPlayer()
    {
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
    



}
