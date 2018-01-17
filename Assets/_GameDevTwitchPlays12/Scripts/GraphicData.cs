using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GraphicData : ScriptableObject
{
    public List<Sprite> avatars;

    public Sprite map;

    public List<GameObject> spawns;
}
