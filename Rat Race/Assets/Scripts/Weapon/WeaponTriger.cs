using UnityEngine;

public class WeaponTriger : MonoBehaviour
{
    [SerializeField] public float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehaviour>().GetDamage(damage);
            Destroy(gameObject);
        }
        
       
    }
}
