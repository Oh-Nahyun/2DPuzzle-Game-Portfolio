using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CardData), true)]
public class CardDataEditor : Editor
{
    CardData cardData;

    private void OnEnable()
    {
        cardData = target as CardData;
    }

    public override void OnInspectorGUI()
    {
        if (cardData != null && cardData.cardIcon != null)
        {
            Texture2D texture;
            EditorGUILayout.LabelField("Item Icon Preview");
            texture = AssetPreview.GetAssetPreview(cardData.cardIcon);
            if (texture != null)
            {
                GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
            }
        }

        base.OnInspectorGUI();
    }
}
#endif
