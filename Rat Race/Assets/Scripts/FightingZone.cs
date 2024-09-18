using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingZone : MonoBehaviour
{
    private bool allEnemyKilled = false;
    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private GameObject[] area;
    private void Update()
    {
        if(enemies.Count == 0) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < area.Length; i++)
            {
                area[i].SetActive(true);
            }

        }
    }
    public void minusEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
    public void PlusEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
   
}
