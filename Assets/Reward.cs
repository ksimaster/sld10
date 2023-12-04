using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Reward : MonoBehaviour
{
    public GameObject gameplay;
    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    // Подписанный метод получения награды
    void Rewarded(int id)
    {
        // Если ID = 0, то следующий уровень
        if (id == 0)
        {
            gameplay.GetComponent<Gameplay>().Next();
            Debug.Log("Следующий уровень за награду");
        }
            
        // Если ID = 1, то то подсказка.
        else if (id == 1)
        {
            gameplay.GetComponent<Gameplay>().Hint();
            Debug.Log("Подсказка за награду");
        }
            
    }

}
