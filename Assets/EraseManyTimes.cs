using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class EraseManyTimes : MonoBehaviour
{
    public List<EraseManyPositionInOneLevel> eraseLevels;

    public GameObject buttonWatchAds;
    public int currentLevel = 0;
    public Transform guidePosition;

    public bool isClickadsable;

    private void Start()
    {
        eraseLevels.UpdateSelected(0, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));

        buttonWatchAds.gameObject.SetActive(false);
        buttonWatchAds.gameObject.transform.localScale = new Vector3(0, 0);
    }

  
    public void FinishDraw()
    {
        if (currentLevel < eraseLevels.Count -1 )
        {
            buttonWatchAds.gameObject.SetActive(true);
           
        }
        
        else {
            EasyEffect.Appear(buttonWatchAds, 0f, 1f);
        }

        if (currentLevel >= eraseLevels.Count - 1)
        {
            buttonWatchAds.gameObject.SetActive(false);
            var eraser = eraseLevels[currentLevel].transform.Find("check_correct").GetComponent<EraserShowPosition>();
            eraser.gameObject.SetActive(false);
            Gameplay.Instance.Win(eraseLevels[eraseLevels.Count - 1],false);
          

            
        }
        else
        {
            currentLevel++;
            if (isClickadsable)
            {
                var go = eraseLevels[currentLevel].transform.Find("check_correct");
                go.gameObject.SetActive(false);
                eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));
                LeanTween.delayedCall(.5f, () =>
                {
                    //LeanTween.scale(buttonWatchAds, new Vector3(.65f, .45f, 1f), .5f).setEase(LeanTweenType.easeOutExpo);
                    EasyEffect.Appear(buttonWatchAds.gameObject, 0f, 1f, speed: 0.2f);
                });

                
            }
            else 
            {
                eraseLevels.UpdateSelected(currentLevel, x => x.gameObject.SetActive(true), x => x.gameObject.SetActive(false));
                for (int i = 0; i < eraseLevels[currentLevel].checkers.Length; i++)
                {
                    eraseLevels[currentLevel].checkers[i].StartChecking();
                }
            }
        }
    }

    public void OnWatchAdsClick()
    {
        buttonWatchAds.gameObject.SetActive(false);

        var go = eraseLevels[currentLevel].transform.Find("check_correct");

        var eraser = go.GetComponent<EraserShowPosition>();
        if (eraser == null)
        {
            go.gameObject.AddComponent<EraserShowPosition>();

        }
        go.gameObject.SetActive(true);

        for (int i = 0; i < eraseLevels[currentLevel].checkers.Length; i++)
        {
            eraseLevels[currentLevel].checkers[i].StartChecking();
        }
        LeanTween.scale(buttonWatchAds, new Vector3(0f, 0f, 0f), 0f).setEase(LeanTweenType.easeInBack);
    }

    public void Hint() {

    }

    public void OnCloseSpecialLevel() {
        Gameplay.Instance.LevelUp();
    }

    public void OnNoButtonSelected()
    {
        LeanTween.scale(buttonWatchAds, new Vector3(0, 0, 0), .5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            Gameplay.Instance.Next();
        }
        );
    }

  
}