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
    [SerializeField] private float distanceSpecialAttack;
    [Header("Mana")]
    [SerializeField] private float hitFromHand;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    private bool canSpecialSkill = true;
    [SerializeField] private GameObject BulletSpecial;
    [SerializeField] private Transform sppBulletSpecial;
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
            StartCoroutine(Attacka()); // когда анимацию делать будешь, настраивать ее, вместа корутина сделай метот и вызывай его тут
        }
        if(distance > distanceAttack && distance < distanceSpecialAttack && isActive && canSpecialSkill)
        {
            StartCoroutine(Special());
        }

    }
    public void OverlapAttack() // чтобы вызывалась в анимации
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
                // isActive = true сюда
            }
        }
    }
    
    IEnumerator Special()
    {
        canSpecialSkill = false;
        isActive = false;
        yield return new WaitForSecondsRealtime(0.3f);
        Instantiate(BulletSpecial, sppBulletSpecial.position, sppBulletSpecial.rotation);
        yield return new WaitForSecondsRealtime(0.3f);
        isActive = true;
        yield return new WaitForSecondsRealtime(10);
        canSpecialSkill = true;
    }
    IEnumerator Attacka() // а тут запускай анимку и isActive = false
    {
        isActive = false;
        yield return new WaitForSeconds(StopBeforAttacka); 
        OverlapAttack(); // эту тему перенеси в анимацию
        isActive = true; // это удали тут и вызывай в методе ОA
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(attackPosition.position, attackRadius);
    }
}
