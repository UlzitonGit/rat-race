using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] Platform[] Plates;
    private List<int> curList = new List<int>();
    [SerializeField] List<int> answer;
    [SerializeField] Transform door;
    [SerializeField] float doorSpeed;
    [SerializeField] Transform targetPos;
    bool isOpen;
    private void Update()
    {
        if (isOpen)
        {
            door.position = Vector3.MoveTowards(door.position, targetPos.position, doorSpeed * Time.deltaTime);
        }
    }
    public void GetCurrentPlatformID(int id)
    {
        if(curList.Count < 4)
        {
            curList.Add(id);
            Debug.Log("Функция отработана");
        }
        if (curList.Count == 4) {
            {
                Debug.Log("Функция отработана, вызвал чек-лист");
                CheckLists();
            }
        }
    }
    private void CheckLists()
    {
        Debug.Log("Чек-лист");
        for (int i = 0; i < curList.Count; i++)
        {
            if (curList[i] != answer[i])
            {
                Debug.Log($"Неправильно {curList[i]}, {answer[i]}");
                curList.Clear();
                StartCoroutine(FIX()); //На этом моменте оффаются плиты, поэтому у игрока будет 2 секунды, чтобы сойти с плиты + без этого будут баги
                return;
            }
        }
        Debug.Log("Правильно");
        isOpen = true;
    }
    IEnumerator FIX()
    {
        yield return new WaitForSeconds(2);
        for (int j = 0; j < 4; j++)
        {
            Plates[j].ChangeColor(1);
            Plates[j].wasActivated = false;
        }
    }
}
