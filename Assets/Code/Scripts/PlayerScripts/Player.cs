using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Toggle toggel;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
          toggel.isOn = false;
        }   else
        {
            toggel.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveFullScreen(bool fullscreenActive)
    {
        Screen.fullScreen = fullscreenActive;
    }
}
