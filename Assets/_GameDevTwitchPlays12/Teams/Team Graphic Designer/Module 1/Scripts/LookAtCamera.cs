using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    void Update () {
        //transform.forward = -Camera.main.transform.forward;
        /*Quaternion newRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position );
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);*/
        /* Quaternion newRotation = Quaternion.LookRotation(Camera.main.transform.position, Vector3.forward);
         newRotation.z = 0.0f;
         //newRotation.y = 0.0f;
         newRotation.x = 0.0f;
         newRotation.w = 0.0f;
         transform.rotation = newRotation;*/

        //transform.LookAt(Camera.main.transform.position, Vector3.back);

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position, Vector3.back);
    }
}
