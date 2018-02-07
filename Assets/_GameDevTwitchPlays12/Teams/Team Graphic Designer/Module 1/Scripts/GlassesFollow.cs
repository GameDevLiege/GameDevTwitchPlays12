using UnityEngine;

public class GlassesFollow : MonoBehaviour {

	public void GlassesFollowCharacter(Vector3 position)
    {
        transform.position = new Vector3(position.x,position.y,transform.position.z);
    }
}
