using UnityEngine;
[RequireComponent (typeof(MeshRenderer))]
public class GpuInctancingEnable : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer mashRenderer = GetComponent<MeshRenderer> ();
        mashRenderer.SetPropertyBlock(materialPropertyBlock);
     }
}
