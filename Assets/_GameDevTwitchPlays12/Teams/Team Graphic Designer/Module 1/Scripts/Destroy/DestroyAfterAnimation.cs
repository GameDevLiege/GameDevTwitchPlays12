using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour {

    private void Start()
    {
        Animation animation = GetComponent<Animation>();
        Destroy(gameObject, animation.clip.length);
    }
}
