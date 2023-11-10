using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using TMPro;

public class CustomLevel49 : FindAndWinLevel
{
    GameObject findObj;

    private void Start()
    {
        Vector3 pos = new Vector3(1.5f, 1.7f, 0f);
        findObj = new GameObject();
        findObj.transform.position = pos;
        objectFinds = new List<GameObject>() { findObj };
        isMultiple = false;
    }

    public override Vector3 GetGuidePosition()
    {
        Vector3 pos = new Vector3(1.5f, 1.7f, 0f);
        return pos;
    }
}