using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDead : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Death();
            Debug.Log("LOgic 1 works");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Death();
            Debug.Log("LOgic 2 works");
        }
    }
}
