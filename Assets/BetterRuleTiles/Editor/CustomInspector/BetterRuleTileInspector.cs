#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VinTools.BetterRuleTiles;
using VinTools.Utilities;
using VinTools.BetterRuleTiles.Internal;

namespace VinToolsEditor.BetterRuleTiles
{
    [CustomEditor(typeof(BetterRuleTile))]
    public class BetterRuleTileInspector : RuleTileEditor
    {
        bool showInspector = false;

        public override void OnInspectorGUI()
        {
            if (!showInspector)
            {
                EditorGUILayout.HelpBox(
                    "This asset was not intended to be edited by users. " +
                    "This asset was generated automatically, therefore there are settings which can be confusing, and which after changed cannot be changed back. " +
                    "Only use this panel for debugging purposes. " +
                    "Changes made in the asset will be lost when generating a new asset.", 
                    MessageType.Warning);
                EditorGUILayout.Space();
                if (GUILayout.Button("Display inspector")) showInspector = true;
            }

            if (showInspector) base.OnInspectorGUI();
        }

        public override void RuleOnGUI(Rect rect, Vector3Int position, int neighbor)
        {
            switch (neighbor)
            {
                case Neighbor.Any: GUI.DrawTexture(rect, TextureUtils.Base64ToTexture(
                    "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"
                    )); return;
                case Neighbor.NotThis: GUI.DrawTexture(rect, arrows[9]); return;
                case Neighbor.Empty: GUI.DrawTexture(rect, TextureUtils.Base64ToTexture(
                    "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"
                    )); return;
                case Neighbor.Ignore: return;
                case Neighbor.This: GUI.DrawTexture(rect, arrows[GetArrowIndex(position)]); return;
                default:
                    var style = new GUIStyle();
                    style.alignment = TextAnchor.MiddleCenter;
                    style.fontSize = 10;
                    GUI.Label(rect, neighbor.ToString(), style);
                    break;
            }

            base.RuleOnGUI(rect, position, neighbor);
        }
    }
}
#endif