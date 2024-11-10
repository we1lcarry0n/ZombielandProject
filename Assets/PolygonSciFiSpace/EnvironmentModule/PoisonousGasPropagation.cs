using UnityEngine;

public class PoisonousGasPropagation : MonoBehaviour
{
    [SerializeField] private GameObject poisonousGas;
    [SerializeField] private float poisonousGasStartTimer;


    void Start()
    {
        Invoke("PoisonousGasStart", poisonousGasStartTimer);
    }

    private void PoisonousGasStart()
    {
        poisonousGas.SetActive(true);
    }
}
