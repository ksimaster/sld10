using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GOgj : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObjectNo;

    void Update()
    {
        if (gameObject1.activeSelf)
        {
            gameObject2.SetActive(true);
            gameObjectNo.SetActive(false);
        }
    }
}