using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetecter : MonoBehaviour
{
    public List<GameObject> enemies;
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
