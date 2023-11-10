﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class UserData
{

    public long nextDay;
    public string userid;
    public string username;
    public float gold = 100;
    public float diamond;
    public int level;
    public int maxLevel;
    public int branchLevel;
    public List<StoryData> storyList;
    public float currentXp;
    public float baseXp;
    //public int monsterInTeam =1;
    public List<string> monstersAvailable;
    public int monsterSlot = 4;
    public List<string> boughtItems;
    public int currentStory;
    public bool playBGM;
    public bool playSound;
    public bool virate;
    public int currentSpecialLevel;
    public bool showRating = false;

    public UserData()
    {
        boughtItems = new List<string>();
        monstersAvailable = new List<string>();
        monstersAvailable.Add("Monster");
    }

    public void Start()
    {


    }

    

   

}