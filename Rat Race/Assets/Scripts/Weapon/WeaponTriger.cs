using UnityEngine;

public class WeaponTriger : MonoBehaviour
{
    [SerializeField] public float damage;
    [HideInInspector] public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && parent != other.gameObject)
        {
            other.GetComponent<EnemyBehaviour>().GetDamage(damage);
            //Destroy(gameObject);
        }
        
       
    }
}
