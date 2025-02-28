using UnityEditor;
[CustomEditor(typeof(UIFlowingLight))]
[CanEditMultipleObjects]
public class UIFlowingLightInspector : Editor
{
    protected UIFlowingLight _target;
    protected virtual void OnEnable()
    {
        _target = target as UIFlowingLight;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();//保留Unity自动生成的Inspector
    }
}