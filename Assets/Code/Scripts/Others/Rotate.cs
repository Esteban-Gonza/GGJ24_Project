using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GameObject child;

    public float speed = 1;
    public float distance = 1;
    public float phase = 0;

    private void Start()
    {
        child.transform.position.Set(10f, 10f, 10f);
    }

    private void Update()
    {
        //vector3 relativePos = new Vector3(
        //    amplitude * Mathf.Sin(Time.realtimeSinceStartup * speed) + phase,
        //    0.0f,
        //    amplitude * Mathf.Cos(Time.realtimeSinceStartup * speed) + phase
        //    );

        //transform.position = relativePos;

        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
