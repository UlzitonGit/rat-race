using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasword : MonoBehaviour
{
    [SerializeField] private GameObject panelPasword;
    private bool neer = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(neer && Input.GetKey(KeyCode.E))
        {
            panelPasword.SetActive(true);
            
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            panelPasword.SetActive(false);
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            neer = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            neer = false;
        }
    }
}
