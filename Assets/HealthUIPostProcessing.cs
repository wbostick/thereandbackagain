using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class HealthUIPostProcessing : MonoBehaviour
{
    Volume volume;
    ChannelMixer channels;
    private void Start() {
        volume = GameObject.FindWithTag("Post").GetComponent<Volume>();
       
        if(volume.profile.TryGet<ChannelMixer>(out channels)) {
            channels.redOutRedIn.value = 100f;
        }
    }
    




    public void setHealthAmmount(float currentHealth) {
        channels.redOutRedIn.value = currentHealth * 2;
    }
}
