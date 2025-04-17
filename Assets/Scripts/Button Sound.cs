using UnityEngine.UI;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    public AudioClip clickSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();      
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", .2f); // Set the volume to the saved value or default to .2
    }

    public void PlaySound() 
    {
        if (clickSound != null && audioSource !=null) 
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
