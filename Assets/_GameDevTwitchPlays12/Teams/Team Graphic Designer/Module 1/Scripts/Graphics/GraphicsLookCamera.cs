using UnityEngine;

public class GraphicsLookCamera : MonoBehaviour {
    
	private void LateUpdate () {
        transform.rotation = Camera.main.transform.rotation;
	}
}
