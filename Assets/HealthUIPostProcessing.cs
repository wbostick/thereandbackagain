using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(Health))]
public class HealthUIPostProcessing : MonoBehaviour
{
    Volume volume;
    ChannelMixer channels;
    private void Start() {
        volume = GameObject.FindWithTag("Post").GetComponent<Volume>();
       
        if(volume.profile.TryGet<ChannelMixer>(out channels)) {
            channels.redOutRedIn.value = 100f;
        }

        Health health = GetComponent<Health>();
        setHealthAmmount(health.currentHealth);

        health.OnDamage.AddListener(setHealthAmmount);
        health.OnHeal.AddListener(setHealthAmmount);
    }

    public void setHealthAmmount(float currentHealth) {
        channels.redOutRedIn.value = ((200 * (200 - currentHealth)) / 200);
        
        channels.redOutGreenIn.value = ((200 * (200 - currentHealth)) / 200) - 100;
        
        channels.redOutBlueIn.value = ((200 * (200 - currentHealth)) / 200) - 100;
    }
}
