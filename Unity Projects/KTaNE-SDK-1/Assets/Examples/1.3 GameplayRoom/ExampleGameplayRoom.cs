using UnityEngine;

public class ExampleGameplayRoom : MonoBehaviour
{
    public Light roomLight;

	void Awake()
    {
        KMGameplayRoom gameplayRoom = GetComponent<KMGameplayRoom>();
        Debug.Log("Setting on light change");
        gameplayRoom.OnLightChange = OnLightChange;
	}

    public void OnLightChange(bool on)
    {
        ticks++;
        if(ticks == 1)
        {
            roomLight.color = new Color(1, 0, 0);
        }
        if (ticks == 2)
        {
            roomLight.color = new Color(0, 0, 1);
            ticks = 0;
        }
    }

    private int ticks = 0;
}
