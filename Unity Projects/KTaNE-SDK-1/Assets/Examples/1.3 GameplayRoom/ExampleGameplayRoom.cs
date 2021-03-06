using System.Collections;
using UnityEngine;
using System;
using System.Reflection;


public class ExampleGameplayRoom : MonoBehaviour
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
        if(ticks == 1) //before the lights come on
        {
            LightsOut();
        }
        if(ticks == 2) //lights initally come on
        {
            LightsOn();
        }
        if(ticks == 3) //lights go out and reset to state 1 (before lights ever came on)
        {
            LightsOut();
            ticks = 1;
        }
        
    }

    public void OneMinuteEvent()
    {
        StartCoroutine(RedLight());
    }

    public IEnumerator RedLight()
    {
        while (true)
        {
            roomLight.GetComponent<Light>().color = new Color(1f, .2f, .2f);
            Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.EmergencyAlarm, gameplayRoom.BombSpawnPosition);
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

    private void LightsOn()
    {
        SetAmbient(new Color(.5f, .5f, .5f));
        roomLight.color = new Color(.5f, .5f, .5f);
        Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.EmergencyAlarm, gameplayRoom.BombSpawnPosition);

    }

    private void LightsOut()
    {
        SetAmbient(new Color(.2f, .2f, .2f));
        roomLight.color = new Color(0, 0, 0);
        Sound.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.EmergencyAlarm, gameplayRoom.BombSpawnPosition);
    }

    private int ticks = 0;
}

