using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private void Start()
    {
        m_rotation = transform.rotation;
    }

    void Update () {
        transform.rotation = m_rotation;
    }

    private Quaternion m_rotation;
}
