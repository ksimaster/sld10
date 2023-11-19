using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    private int i = 0;
    public float speedTime;
    void Start()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        while (i == 0)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(speedTime);
            gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            yield return new WaitForSeconds(speedTime);
            gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            yield return new WaitForSeconds(speedTime);
            gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

    }
}
