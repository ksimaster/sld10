using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Reward : MonoBehaviour
{
    public GameObject gameplay;
    // ������������� �� ������� �������� ������� � OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    // ������������ �� ������� �������� ������� � OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    // ����������� ����� ��������� �������
    void Rewarded(int id)
    {
        // ���� ID = 0, �� ��������� �������
        if (id == 0)
        {
            gameplay.GetComponent<Gameplay>().Next();
            Debug.Log("��������� ������� �� �������");
        }
            
        // ���� ID = 1, �� �� ���������.
        else if (id == 1)
        {
            gameplay.GetComponent<Gameplay>().Hint();
            Debug.Log("��������� �� �������");
        }
            
    }

}
