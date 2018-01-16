using UnityEngine;
using UnityEditor;
using System.Linq;

/// <summary>
/// Groups selected GameObjects together.
/// </summary>
/// <remarks>
/// Inspired from https://github.com/heisarzola/Unity-Development-Tools/blob/master/Tools/Editor/Group%20Utility/GroupUtility.cs
/// </remarks>
public class GroupGameObjects : Editor
{
    #region Public Members

    #endregion

    #region Public void

    // %g = Ctrl+G
    [MenuItem("Edit/Group selected GameObjects together %g", false)]
    public static void Group()
    {
        if (Selection.objects.Length < 1)
        {
            Debug.LogWarning("No gameObjects selected to group together!");
            return;
        }

        GameObject group = new GameObject("Group");

        Undo.RegisterCreatedObjectUndo(group, "Grouped selected GameObjects");

        Bounds boundBox = new Bounds();
        
        foreach (Transform tr in Selection.transforms)
            boundBox.Encapsulate(tr.position);

        // Set the parent's pivot at the center of the selected GameObjects
        group.transform.position = boundBox.center;

        // Technical: Can't group both foreach together
        foreach(Transform tr in Selection.transforms)
            Undo.SetTransformParent(tr.transform, group.transform, "Moved selected gameObject to its newly created parent");

        Selection.activeGameObject = group;
    }

    #endregion

    #region System

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
