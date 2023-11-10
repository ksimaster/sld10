﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using DarkcupGames;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    public static UserData  userdata;

    public const string USER_DATA_FILE_NAME = "on7PRwy5XiwWj3z6";

    public TextMeshProUGUI txtGold;
    public TextMeshProUGUI txtDiamond;
    public UIUpdater userUpdater;

    public System.Type userDataType;

    void Awake()
    {
        Instance = this;
        LoadUserData();
    }

    private void Start()
    {
        CheckSoundMusic();
        if (txtGold)
        {
            txtGold.text = userdata.gold.ToString();
        }
        if (txtDiamond)
        {
            txtDiamond.text = userdata.diamond.ToString();
        }
        if (userUpdater)
        {
            if (userdata.username == null || userdata.username == "")
            {
                userdata.username = "User";
                GameSystem.SaveUserDataToLocal();
            }
            userUpdater.UpdateUI(userdata, userUpdater.gameObject);
        }
    }

   

    public void AddGold(long amount)
    {
        StartCoroutine(IEIncreaseNumber(txtGold, (long)userdata.gold, (long)userdata.gold + amount, 0.2f));
        userdata.gold += amount;
        SaveUserDataToLocal();
    }

    public static Vector3 GoToTargetVector(Vector3 current, Vector3 target, float speed, bool isFlying = false)
    {
        float distanceToTarget = Vector3.Distance(current, target);
        if (distanceToTarget < 0.1f)
            return new Vector3(0, 0);

        Vector3 vectorToTarget = target - current;

        vectorToTarget = vectorToTarget * speed / distanceToTarget;

        return vectorToTarget;
    }

    public IEnumerator IEIncreaseNumber(TextMeshProUGUI txtNumber, long startGold, long endGold, float effectTime, string endText = "")
    {
        int increase = (int)((endGold - startGold) / (effectTime / Time.deltaTime));
        if (increase == 0)
        {
            increase = endGold > startGold ? 1 : -1;
        }
        long gold = startGold;
        bool loop = true;
        while (loop)
        {
            gold += increase;

            if (startGold < endGold)
            {
                loop = gold < endGold;
            }
            else
            {
                loop = gold > endGold;
            }

            txtNumber.text = gold.ToString() + endText;

            yield return new WaitForEndOfFrame();
        }
    }

    public static void SaveUserDataToLocal()
    {
        string json = JsonConvert.SerializeObject(GameSystem.userdata);
        PlayerPrefs.SetString("JSON", json);
        //string path = FileUtilities.GetWritablePath(GameSystem.USER_DATA_FILE_NAME);

        //FileUtilities.SaveFile(System.Text.Encoding.UTF8.GetBytes(json), path, true);
    }

    public static void LoadUserData()
    {
        if (!PlayerPrefs.HasKey("JSON"))//!FileUtilities.IsFileExist(GameSystem.USER_DATA_FILE_NAME))
        {
            GameSystem.userdata = new UserData();
            GameSystem.SaveUserDataToLocal();
        }
        else
        {
            //GameSystem.userdata = FileUtilities.DeserializeObjectFromFile<UserData>(GameSystem.USER_DATA_FILE_NAME);
            GameSystem.userdata = FileUtilities.DeserializeObjectFromText<UserData>(PlayerPrefs.GetString("JSON"));
           // PlayerPrefs.GetString("JSON");
        }
    }
    public void ClickButtonMusic()
    {
        userdata.playBGM = !GameSystem.userdata.playBGM;

        AudioSystem.Instance.SetBGM(GameSystem.userdata.playBGM);
        SaveUserDataToLocal();
    }

    public void CheckSoundMusic()
    {
        AudioSystem.Instance.SetFXSound(GameSystem.userdata.playSound);
        AudioSystem.Instance.SetBGM(GameSystem.userdata.playBGM);
    }

    public void ClickButtonSound()
    {
        userdata.playSound = !GameSystem.userdata.playSound;
        AudioSystem.Instance.SetFXSound(GameSystem.userdata.playSound);
        SaveUserDataToLocal();
    }

    public void ClickVirateButton()
    {
        userdata.virate = !userdata.virate;
        Debug.Log(userdata.virate);
        SaveUserDataToLocal();
    }
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    //public static float CalculateNextLevelExp(float currentMaxExp, int level)
    //{
    //    return currentMaxExp + Constant.BASE_RATIO * Mathf.Pow(Constant.LEVEL_RATIO, level); 
    //}
}