using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool wasActivated = false;
    [SerializeField] int id;
    [SerializeField] PlatformManager manager;
    [SerializeField] Material defMaterial;
    [SerializeField] Material activeMaterial;
    private void OnTriggerEnter(Collider other)
    {
            if (other.GetComponent<Collider>().CompareTag("Player") && !wasActivated)
            {
            Debug.Log("Вызвал функцию GetCurrentPlatformID");
                manager.GetCurrentPlatformID(id);
                wasActivated = true;
                ChangeColor(2);
            }
    }
    public void ChangeColor(int situation)
    {
        if (situation == 1) // для офа (см PlatformManager)
        {
            GetComponent<Renderer>().material = defMaterial;
        }
        if (situation == 2) // для включения
        {
            GetComponent<Renderer>().material = activeMaterial;
        }                     
    }
    
}
