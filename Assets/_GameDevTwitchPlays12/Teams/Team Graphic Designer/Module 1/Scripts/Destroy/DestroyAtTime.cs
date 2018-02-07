using UnityEngine;

public class DestroyAtTime : MonoBehaviour {
    
    public float m_TimeToDestroy;

	void Start () {
        Destroy(gameObject, m_TimeToDestroy);
	}
}
