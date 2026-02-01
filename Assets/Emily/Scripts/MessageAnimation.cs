using UnityEngine;
using System.Collections;

public class MessageAnimation : MonoBehaviour
{
    [SerializeField] private float[] msgCooldowns;
    [SerializeField] private GameObject[] msgs;
    
    [SerializeField] private GameObject[] thingsToDisable;
    [SerializeField] private GameObject panningCamera;

    void OnEnable()
    {
        StartCoroutine(ShowMessages());
    }

    IEnumerator ShowMessages()
    {
        for(int i=0;i<msgs.Length;i++)
        {
            yield return new WaitForSeconds(msgCooldowns[i]);
            msgs[i].SetActive(true);
        }
    }

    public void Ok()
    {
        panningCamera.SetActive(true);
        foreach(GameObject obj in thingsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
