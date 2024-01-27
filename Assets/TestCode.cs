using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.laughometer.value -= GameManager.Instance.decreaseAmount;
        }
    }

    public void GetLaughometerIncrease()
    {
        GameManager.Instance.laughometer.value++;
    }
}