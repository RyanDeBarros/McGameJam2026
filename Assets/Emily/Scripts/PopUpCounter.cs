using UnityEngine;
using System.Collections;

public class PopUpCounter : MonoBehaviour
{
    [SerializeField] private float cooldown = 4;
    [SerializeField] private GameObject popup;

    void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(cooldown);

        popup.SetActive(true);
    }
}
