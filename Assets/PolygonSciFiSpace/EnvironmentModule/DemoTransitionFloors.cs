using UnityEngine;

public class DemoTransitionFloors : MonoBehaviour
{
    public GameObject level;
    public GameObject foog;
    public GameObject triggerUp;
    public GameObject triggerDown;


    public bool isActiveLevel = false;
    public bool isActiveFoog = false;
    public bool isTriggerUp = false;
    public bool isTriggerDown = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            level.SetActive(isActiveLevel);
            foog.SetActive(isActiveFoog);
            triggerUp.SetActive(isTriggerUp);
            triggerDown.SetActive(isTriggerDown);
        }
    }

}
