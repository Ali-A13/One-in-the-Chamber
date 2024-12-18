using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Link to your UI slider

    void Start()
    {
        // Set initial volume from PlayerPrefs or default to 1 (max volume)
        float volume = PlayerPrefs.GetFloat("Volume", 1f); // Default to 1 if no saved value
        volumeSlider.value = volume;
        AudioListener.volume = volume;

        // Add listener to update volume when slider changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // This method will be called whenever the slider value changes
    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // Update global volume
        PlayerPrefs.SetFloat("Volume", value); // Save the volume setting for next time
    }
}

