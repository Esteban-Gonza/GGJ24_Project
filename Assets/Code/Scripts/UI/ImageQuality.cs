using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class LogicaCalidad : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int Calidad;

    // Start is called before the first frame update
    void Start()
    {
        Calidad = PlayerPrefs.GetInt("NumberQuality", 3);
        dropdown.value = Calidad;
        JustQuality();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JustQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("NumberQuality", dropdown.value);
        Calidad = dropdown.value;       
    }
}
