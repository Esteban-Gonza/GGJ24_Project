using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableObjects : MonoBehaviour
{
    [SerializeField] GameObject objectToCollect;
    [SerializeField] TypeOfCollectable type;

    public enum TypeOfCollectable
    {
        None,
        Doll,
        Baseball,
        Radio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToCollect.SetActive(false);

            switch(type) 
            {
                case TypeOfCollectable.Doll:
                    Debug.Log("Doll collected");
                    break;
                case TypeOfCollectable.Baseball:
                    Debug.Log("Baseball collected");
                    break;
                case TypeOfCollectable.Radio:
                    Debug.Log("Radio collected");
                    break;
                default:
                    break;
            }
        }
    }
}