using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreamerDestroy : MonoBehaviour
{
    public VideoPlayer video;

    private void Awake()
    {
        Destroy(gameObject, (float)video.length);
    }
}
