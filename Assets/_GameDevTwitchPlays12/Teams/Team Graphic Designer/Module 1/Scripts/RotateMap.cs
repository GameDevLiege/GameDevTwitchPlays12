using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour {

	void Start () {
        /*Quaternion newRot = gameObject.transform.rotation;
        gameObject.transform.rotation = new Quaternion(newRot.x*2, newRot.y * 2, newRot.z * 2, newRot.w * 2);*/
        gameObject.transform.Rotate(90f,0,0);

    }
}
