using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrap : MonoBehaviour
{
    private bool playerInsideTrigger = false;

    [SerializeField] private float increaseLaughAmount = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            GameManager.Instance.laugherValue++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInsideTrigger)
        {
            GameManager.Instance.laugherValue += increaseLaughAmount * Time.deltaTime;
        }
    }
}