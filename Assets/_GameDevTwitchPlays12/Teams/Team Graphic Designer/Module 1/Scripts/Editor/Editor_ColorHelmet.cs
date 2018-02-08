using UnityEditor;

[CustomEditor(typeof(ColorHelmet))]
public class Editor_ColorHelmet : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorHelmet helmet = target as ColorHelmet;
        helmet.UpdateColor(helmet.m_ColorHelmet);
    }
}
