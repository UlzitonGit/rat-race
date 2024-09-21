using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApathy : EnemyBehaviour
{
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float StopBeforAttacka;


    [SerializeField] private float distanceAttack;
    [Header("Mana")]
    [SerializeField] private float hitFromHand;
    private GameObject ManaUIScript;
    private ManaUI manaUI;

    // Start is called before the first frame update
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SearchForPlayer();
        if(distance < distanceAttack && isActive)
        {
            StartCoroutine(Attacka());
        }
    }
    private void OverlapAttack()
    {
        Collider[] overlapEnemy = Physics.OverlapSphere(attackPosition.position, attackRadius, player);
        for (int i = 0; i < overlapEnemy.Length; i++)
        {
            Vector3 Enemy = transform.position;
            Vector3 Player = overlapEnemy[i].transform.position;
            bool ObstaclesBeforEnemy = Physics.Linecast(Player, Enemy, obstacles.value);
            if (!ObstaclesBeforEnemy)
            {
                manaUI.mana -= hitFromHand;
            }
        }
    }
    IEnumerator Attacka()
    {
        isActive = false;
        yield return new WaitForSeconds(StopBeforAttacka);
        OverlapAttack();
        isActive = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(attackPosition.position, attackRadius);
    }
}
