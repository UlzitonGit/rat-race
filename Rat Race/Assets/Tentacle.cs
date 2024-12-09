using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    PlayerController playerController;
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        transform.LookAt(playerController.transform.position);
    }
   
  
}
