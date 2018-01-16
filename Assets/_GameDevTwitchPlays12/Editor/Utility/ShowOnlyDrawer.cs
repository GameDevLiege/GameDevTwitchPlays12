using UnityEngine;
using UnityEditor;

/// <summary>
///     Allows to add a [ShowOnly] drawer on attributes we wish to make read-only in the inspector.
/// </summary>
/// <remarks> Original source: https://answers.unity.com/answers/793967/view.html </remarks>
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        string valueStr;

        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueStr = prop.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                valueStr = prop.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                valueStr = prop.floatValue.ToString("0.00000");
                break;
            case SerializedPropertyType.String:
                valueStr = prop.stringValue;
                break;
            case SerializedPropertyType.Vector2:
                valueStr = "X " + prop.vector2Value.x + ", Y " + prop.vector2Value.y;
                break;
            case SerializedPropertyType.Vector3:
                valueStr = "X " + prop.vector3Value.x + ", Y " + prop.vector3Value.y + ", Z " + prop.vector3Value.z;
                break;
            default:
                valueStr = "(not supported)";
                break;
        }

        EditorGUI.LabelField(position, label.text, valueStr);
    }
}