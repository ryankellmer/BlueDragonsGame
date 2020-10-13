

using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPRef";

    public float backgroundFloat, soundEffectFloat;

    public AudioSource backgroundAudio;
    public AudioSource[] soundEffectAudio; //can set to however many audio sources you need assigned in GUI


    // Start is called before the first frame update
    void Awake()
    {
        ContinueSettings();
    }
    
    private void ContinueSettings() 
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
        
        
        
        
        backgroundAudio.volume = backgroundFloat;

        for (int i = 0; i < soundEffectAudio.Length; i++)
        {
            soundEffectAudio[i].volume = soundEffectFloat;
        }
    }
   
}
