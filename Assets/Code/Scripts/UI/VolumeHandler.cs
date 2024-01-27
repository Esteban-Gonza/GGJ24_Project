using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private Image muteImage;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float sliderValue;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = volumeSlider.value;
        CheckMute();
    }

    public void ChangeSliderValue(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = volumeSlider.value;
        CheckMute();
    }

    private void CheckMute()
    {
        if(sliderValue == 0)
        {
            muteImage.enabled = true;
        }
        else
        {
            muteImage.enabled = false;
        }
    }
}