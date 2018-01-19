using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    void Start () {

        transform.LookAt(Camera.main.transform.position, Vector3.back);
    }
}
