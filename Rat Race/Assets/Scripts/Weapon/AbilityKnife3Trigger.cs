using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityKnife3Trigger : MonoBehaviour
{
    [SerializeField] public float damage;
    private float realDamage;
    
    [HideInInspector] public float countEnemy = 0;
    private void Start()
    {
        realDamage = damage;
    }
    private void Update()
    {
        if(countEnemy >= 5)
        {
            countEnemy = 5;
        }
        realDamage = damage + (damage / 100 * 20 * countEnemy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            other.GetComponent<EnemyBehaviour>().GetDamage(realDamage);
            countEnemy++;
            //Debug.Log("Hit");
            //Destroy(gameObject);
        }
        


    }
}
