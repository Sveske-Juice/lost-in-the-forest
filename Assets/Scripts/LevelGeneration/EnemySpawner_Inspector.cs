#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawner_Inspector : Editor
{
    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        enemySpawner = target as EnemySpawner;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Bake"))
        {
            enemySpawner.SpawnEnemies();
        }
    }
}

#endif
