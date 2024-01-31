using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerSpawn : MonoBehaviour
{
    public GameObject[] screamerPrefab;
    public GameObject playerCameraRoot;

    private void Awake()
    {
        playerCameraRoot = GameObject.Find("PlayerCameraRoot");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.changeLaughterValue(15f);
            GameObject obj = Instantiate(screamerPrefab[Random.Range(0, screamerPrefab.Length)]);
            obj.transform.parent = playerCameraRoot.transform;
            
            Destroy(gameObject);
        }
    }
}
