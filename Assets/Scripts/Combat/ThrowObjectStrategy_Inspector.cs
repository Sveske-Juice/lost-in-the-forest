#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThrowObjectStrategy))]
public class ThrowObjectStrategy_Inspector : Editor
{
    private ThrowObjectStrategy throwObjectStrategy;

    private void OnEnable()
    {
        throwObjectStrategy = target as ThrowObjectStrategy;
    }

    public override void OnInspectorGUI()
    {
        float throwDist =
            CurveFollower.ThrowDistance(throwObjectStrategy.ThrowForce, throwObjectStrategy.ThrowAngle * Mathf.Deg2Rad, throwObjectStrategy.Gravity);
        float airTime =
            CurveFollower.ThrowTime(throwObjectStrategy.ThrowForce, throwObjectStrategy.ThrowAngle * Mathf.Deg2Rad, throwObjectStrategy.Gravity);

        EditorGUILayout.LabelField($"Throw Range: {throwDist}u");
        EditorGUILayout.LabelField($"Air time (time from throw to ground impact): {airTime}s");

        base.OnInspectorGUI();
    }
}
#endif
