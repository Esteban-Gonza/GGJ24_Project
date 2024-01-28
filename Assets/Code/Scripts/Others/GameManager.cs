using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    [SerializeField] Slider laughometerSlider;
    [SerializeField] private float decreaseLaughAmount;

    [SerializeField] private Animator deathUIAnimator;
    [SerializeField] private GameObject playerHUD;

    public float laugherValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        laughometerSlider.value = laugherValue;

    }

    private void Update()
    {
        if(remainingTime > 0) 
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
        }

        laughometerSlider.value = laugherValue;

        if (Input.GetKeyDown(KeyCode.X))
        {
            laugherValue -= decreaseLaughAmount;
        }

        if(Input.GetKeyDown(KeyCode.Y)) 
        {
            Death();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Death()
    {
        playerHUD.SetActive(false);
        deathUIAnimator.SetTrigger("Death");
    }
}