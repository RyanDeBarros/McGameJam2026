using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject msg;

    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private float minX,maxX,minY,maxY;
    [SerializeField] private Transform prefabParent;

    void OnEnable()
    {
        //play sound here
    }

    public void ClickHere()
    {
        msg.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        //generate prefab of popup
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        GameObject instance = Instantiate(popupPrefab, new Vector3(x,y,0),Quaternion.identity, prefabParent);
        instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(x,y,0);
        Destroy(gameObject);


    }
}
