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
                for (int j = 0; j < 4; j++)
                {
                    Plates[j].ChangeColor(1);
                    Plates[j].wasActivated = false;
                }
                return;
            }
        }
        Debug.Log("Правильно");
        door.position = Vector3.MoveTowards(door.position, targetPos.position, doorSpeed);
    }
}
