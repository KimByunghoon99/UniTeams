using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
public class volume_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl(){
        float sound = audioSlider.value;
        if(sound == -40f){  
            masterMixer.SetFloat("Bgm",-80);
        }
        else{
            masterMixer.SetFloat("Bgm",sound);
        }
    }
    public void ToggleAudioVolume(){
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
