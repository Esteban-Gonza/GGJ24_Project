using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreamerDestroy : MonoBehaviour
{
    public VideoPlayer video;

    private void Awake()
    {
        transform.localPosition = new Vector3(0, 0, 1.6f);

        Destroy(gameObject, (float)video.length);
    }
}
