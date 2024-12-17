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
    }

    public void PlaySound() 
    {
        if (clickSound != null && audioSource !=null) 
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
