using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotgun : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.LookAt(player.transform.position);
       
    }
}
