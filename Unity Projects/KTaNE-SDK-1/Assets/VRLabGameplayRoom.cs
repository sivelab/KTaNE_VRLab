using System.Collections;
using UnityEngine;
using System;
using System.Reflection;


public class VRLabGameplayRoom : MonoBehaviour
{
    public Light roomLight;
    public KMAudio Sound;
    public KMGameplayRoom gameplayRoom;

    void Awake()
    {
        gameplayRoom = GetComponent<KMGameplayRoom>();
        Sound = GetComponent<KMAudio>();

        Debug.Log("Setting on light change");
        gameplayRoom.OnLightChange = OnLightChange;

    }

    public void OnLightChange(bool on)
    {
        ticks++;
        if (ticks == 1) //before the lights come on
        {
            LightsOut(false);
        }
        if (ticks == 2) //lights initally come on
        {
            LightsOn(true);
        }
        if (ticks == 3) //lights go out and reset to state 1 (before lights ever came on)
        {
            LightsOut(true);
        }
        if (ticks == 4) //lights come back on
        {
            LightsOn(true);
        }
        if(ticks == 5) //one minute event begins
        {
            OneMinuteEvent();
        }

    }

    public void OneMinuteEvent()
    {
        SetAmbient(new Color(.2f, .2f, .2f));
        StartCoroutine(RedLight());
    }

    public IEnumerator RedLight()
    {
        while (true)
        {
            roomLight.GetComponent<Light>().color = new Color(1f, .2f, .2f);
            Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.EmergencyAlarm, roomLight.transform);
            yield return new WaitForSeconds(1);
            roomLight.GetComponent<Light>().color = new Color(.5f, .5f, .5f);
            yield return new WaitForSeconds(1.25f);
        }
    }

    public void SetAmbient(Color c)
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = c;
        RenderSettings.ambientIntensity = 1;
        RenderSettings.ambientSkyColor = c;
        DynamicGI.updateThreshold = 0;
        DynamicGI.UpdateEnvironment();

    }

    private void LightsOn(bool audio)
    {
        SetAmbient(new Color(.5f, .5f, .5f));
        roomLight.color = new Color(.5f, .5f, .5f);
        if(audio)
        {
            Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.LightBuzzShort, roomLight.transform);
        }
    }

    private void LightsOut(bool audio)
    {
        SetAmbient(new Color(.2f, .2f, .2f));
        roomLight.color = new Color(0, 0, 0);
        if(audio)
        {
            Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.LightBuzz, roomLight.transform);
        }
    }

    private int ticks = 0;
}

