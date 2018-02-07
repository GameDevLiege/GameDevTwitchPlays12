using UnityEngine;

public class ObjectsFollow : MonoBehaviour {

    public static void FollowCharacter(GameObject PrefabsObject, Vector3 position)
    {
        FollowCharacter(PrefabsObject.transform, position);
    }

    public static void FollowCharacter(Transform PrefabsObject, Vector3 position)
    {
        PrefabsObject.position = new Vector3(position.x, position.y, PrefabsObject.position.z);
    }
}
