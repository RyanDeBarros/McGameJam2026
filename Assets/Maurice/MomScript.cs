using UnityEngine;

public class MomScript : MonoBehaviour
{
    public string booleanString = "stunTrigger";
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("horse");
        if (other.gameObject.tag == "TheActualSpray")
        {
            gameObject.GetComponent<Animator>().SetTrigger(booleanString);
        }
    }
}
