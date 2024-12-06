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
            Debug.Log("������� ����������");
        }
        if (curList.Count == 4) {
            {
                Debug.Log("������� ����������, ������ ���-����");
                CheckLists();
            }
        }
    }
    private void CheckLists()
    {
        Debug.Log("���-����");
        for (int i = 0; i < curList.Count; i++)
        {
            if (curList[i] != answer[i])
            {
                Debug.Log($"����������� {curList[i]}, {answer[i]}");
                curList.Clear();
                StartCoroutine(FIX()); //�� ���� ������� �������� �����, ������� � ������ ����� 2 �������, ����� ����� � ����� + ��� ����� ����� ����
                return;
            }
        }
        Debug.Log("���������");
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
