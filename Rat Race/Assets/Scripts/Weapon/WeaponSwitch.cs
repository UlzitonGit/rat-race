using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] GameObject knifeModel;
    [SerializeField] GameObject bowModel;
    [SerializeField] GameObject launcherModel;
    [SerializeField] Animator animator;
    Knife knife;
    Bow bow;
    Launcher launcher;
    private void Start()
    {
        knife = GetComponent<Knife>();
        bow = GetComponent<Bow>();
        launcher = GetComponent<Launcher>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            animator.SetBool("Launcher", false);
            bow.Switch();
            knife.enabled = true;
            knifeModel.SetActive(true);
            launcherModel.SetActive(false);
            bow.enabled = false;
            bowModel.SetActive(false);
            launcher.enabled = false;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            animator.SetBool("Launcher", false);
            knife.enabled = false;
            knifeModel.SetActive(false);
            launcherModel.SetActive(false);
            bow.enabled = true;
            bowModel.SetActive(true);
            launcher.enabled = false;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            animator.SetBool("Launcher", true);
            launcher.enabled = true;
            bow.Switch();
            knife.enabled = false;
            knifeModel.SetActive(false);
            launcherModel.SetActive(true);
            bow.enabled = false;
            bowModel.SetActive(false);
        }
    }
}
