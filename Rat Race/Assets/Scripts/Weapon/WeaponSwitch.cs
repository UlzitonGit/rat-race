using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] GameObject knifeModel;
    [SerializeField] GameObject bowModel;
    Knife knife;
    Bow bow;
    private void Start()
    {
        knife = GetComponent<Knife>();
        bow = GetComponent<Bow>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            bow.Switch();
            knife.enabled = true;
            knifeModel.SetActive(true);
            bow.enabled = false;
            bowModel.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            knife.enabled = false;
            knifeModel.SetActive(false);
            bow.enabled = true;
            bowModel.SetActive(true);
        }
    }
}
