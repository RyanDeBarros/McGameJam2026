using UnityEngine;
using System.Collections;

public class BabyAnimation : MonoBehaviour
{
    [SerializeField] Mesh Spr1Mse;
    [SerializeField] Mesh Spr2Mse;
    [SerializeField] Material Spr1Mt;
    [SerializeField] Material Spr2Mt;
    [SerializeField] float swapTime = 0.5f;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            // First sprite
            meshFilter.mesh = Spr1Mse;
            meshRenderer.material = Spr1Mt;
            //Debug.Log("Swapping to sprite 1");

            yield return new WaitForSeconds(swapTime);

            // Second sprite
            meshFilter.mesh = Spr2Mse;
            meshRenderer.material = Spr2Mt;
            //Debug.Log("Swapping to sprite 2");

            yield return new WaitForSeconds(swapTime);
        }
    }
}
