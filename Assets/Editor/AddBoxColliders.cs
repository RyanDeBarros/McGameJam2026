using UnityEngine;
using UnityEditor;

public class AddBoxColliders
{
    [MenuItem("Tools/Add Box Colliders To Scene Objects")]
    static void AddColliders()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        int addedCount = 0;

        foreach (GameObject obj in allObjects)
        {
            // Skip objects not in scene (like prefabs in project)
            if (!obj.scene.IsValid())
                continue;

            string nameLower = obj.name.ToLower();

            // Skip by name
            if (nameLower.Contains("streetlight") || nameLower.Contains("tree"))
                continue;

            // Skip lights
            if (obj.GetComponent<Light>() != null)
                continue;

            // Skip UI
            if (obj.GetComponent<RectTransform>() != null)
                continue;

            // Skip if no mesh (avoids empty parents)
            if (obj.GetComponent<MeshRenderer>() == null)
                continue;

            // Skip if already has any collider
            if (obj.GetComponent<Collider>() != null)
                continue;

            Undo.AddComponent<BoxCollider>(obj);
            addedCount++;
        }

        Debug.Log($"Added BoxColliders to {addedCount} objects.");
    }
}
