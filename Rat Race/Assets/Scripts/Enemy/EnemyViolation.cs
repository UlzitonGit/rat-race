using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyViolation : EnemyBehaviour
{
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private float StopBeforAttacka;


    [SerializeField] private float distanceAttack;

    [SerializeField] private float timeBeforeAggession;
    [SerializeField] private float plusDamage;
    [Header("Mana")]
    [SerializeField] private float hitFromHand;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    private bool isAgresiv = false;
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            SearchForPlayer();
            if (!isAgresiv)
            {
                isActive = true;
                StartCoroutine(Aggression());

            }
        }
        
        
        if (distance < distanceAttack && isActive)
        {
            StartCoroutine(Attacka()); // ����� �������� ������ ������, ����������� ��, ������ �������� ������ ����� � ������� ��� ���
            //transform.LookAt(Player.transform);
        }
    }
    public void OverlapAttack() // ����� ���������� � ��������
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
                // isActive = true ����
            }
        }
    }
    IEnumerator Attacka() // � ��� �������� ������ � isActive = false
    {
        isActive = false;
        yield return new WaitForSeconds(StopBeforAttacka);
        
        OverlapAttack(); // ��� ���� �������� � ��������
        yield return new WaitForSeconds(StopBeforAttacka);
        isActive = true; // ��� ����� ��� � ������� � ������ �A
    }
    IEnumerator Aggression()
    {
        yield return new WaitForSecondsRealtime(timeBeforeAggession);
        speed += 2.5f;
        hitFromHand += plusDamage;
        currentEnemyHealth /= 1.6f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(attackPosition.position, attackRadius);
    }
}
