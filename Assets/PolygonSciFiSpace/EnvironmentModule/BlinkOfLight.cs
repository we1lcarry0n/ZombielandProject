using UnityEngine;

public class BlinkOfLight : MonoBehaviour
{
    [SerializeField] private GameObject light;
    [SerializeField] private float flickerOnMin;
    [SerializeField] private float flickerOnMax;

    [SerializeField] private float flickerOffMin;
    [SerializeField] private float flickerOffMax;

    void Awake() => LightOn();  
    
    void LightOn()
    {
        light.SetActive(true);
        Invoke("LightOff", Random.Range(flickerOnMin, flickerOnMax));
    }

    void LightOff()
    {
        light.SetActive(false);
        Invoke("LightOn", Random.Range(flickerOffMin, flickerOffMax));
    }
}

