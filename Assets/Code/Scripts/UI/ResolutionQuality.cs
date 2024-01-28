using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
using TMPro;

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle toggle;


    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;

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

        // Llama a RevisarResolucion después de establecer el estado del toggle
        RevisarResolucion();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveFullScreen(bool Active)
    {
        Screen.fullScreen = Active;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
             
        }
        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();

        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", resolucionesDropDown.value);

    }

    public void cambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        Resolution resolucion = resoluciones[resolucionesDropDown.value];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }


}