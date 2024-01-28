using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string gameScene;
   public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Endgame ()
    {
        Application.Quit();
    }
}
