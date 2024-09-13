using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] private float health;
    private float currentEnemyHealth;
    EnemyDetecter detecter;
    // Start is called before the first frame update
    void Awake()
    {
        currentEnemyHealth = health;
        detecter = FindObjectOfType<EnemyDetecter>();
    }

    // Update is called once per frame
   
    public void GetDamage(float damage)
    {
        currentEnemyHealth -= damage;
        healthBar.fillAmount = currentEnemyHealth / health;
        print(currentEnemyHealth);
        if(currentEnemyHealth <= 0)
        {
            detecter.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
