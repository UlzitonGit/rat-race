using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] public float health;
    private float currentEnemyHealth;
    private GameObject EnemyDetecterScript;
    EnemyDetecter detecter;
    FightingZone zone;
    // Start is called before the first frame update
    void Awake()
    {
        
        
        EnemyDetecterScript = GameObject.FindWithTag("Detecter");
        currentEnemyHealth = health;
        detecter = EnemyDetecterScript.GetComponent<EnemyDetecter>();
    }

    // Update is called once per frame
   
    public void GetDamage(float damage)
    {
        currentEnemyHealth -= damage;
        healthBar.fillAmount = currentEnemyHealth / health;
        //print(currentEnemyHealth);
        if(currentEnemyHealth <= 0)
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
