using UnityEngine;

public class WeaponTriger : MonoBehaviour
{
    [SerializeField] public float damage;
    [HideInInspector] public GameObject parent;
    [SerializeField] public bool isBullet;
    
    [SerializeField] private Transform tpBulletDable;
    
    [SerializeField] private Transform sppDable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && parent != other.gameObject)
        {
            other.GetComponent<EnemyBehaviour>().GetDamage(damage);

            //Destroy(gameObject);
        }
        if(other.CompareTag("Dable") && isBullet)
        {
            transform.position = tpBulletDable.transform.position;
            
            GameObject link = Instantiate(gameObject, sppDable.transform.position, sppDable.transform.rotation);
            link.GetComponent<WeaponTriger>().isBullet = false;
        }
        
       
    }
}
