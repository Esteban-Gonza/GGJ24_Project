using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using TMPro;

public class LogicaFullScrean : MonoBehaviour
{

    public Toggle toggle;

    public TMP_Dropdown ResolutionsDropDown;
    Resolution[] Resoluciones;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        CheckSolution();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveFullScreen(bool Active)
    {
        Screen.fullScreen = Active;
    }

    public void CheckSolution()
    {
        Resoluciones = Screen.resolutions;
        ResolutionsDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int CurrentResolutions = 0;


        for (int i = 0; i < Resoluciones.Length; i++)
        {
            string option = Resoluciones[i].width + " x " + Resoluciones[i].height;
            opciones.Add(option);
                
                
                
            if(Screen.fullScreen && Resoluciones[i].width == Screen.currentResolution.width &&
               Resoluciones[i].height == Screen.currentResolution.height)
            {
                CurrentResolutions = i;
            }
        }

        ResolutionsDropDown.AddOptions(opciones);
        ResolutionsDropDown.value = CurrentResolutions;
        ResolutionsDropDown.RefreshShownValue();
    }

    public void ChangeResolutions(int IndiceResolucion)
    {

        PlayerPrefs.SetInt(" ", ResolutionsDropDown.value);

        Resolution Resolucion = Resoluciones[IndiceResolucion];
        Screen.SetResolution(Resolucion.width, Resolucion.height, Screen.fullScreen);
    }
}
