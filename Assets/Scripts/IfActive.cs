using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfActive : MonoBehaviour
{
    void Start()
    {
        
    }

    private void LoadSound()
    {
        gameObject.SetActive(GameSystem.userdata.playSound);
    }
    private void LoadMusic()
    {
        gameObject.SetActive(GameSystem.userdata.playBGM);
    }
}
