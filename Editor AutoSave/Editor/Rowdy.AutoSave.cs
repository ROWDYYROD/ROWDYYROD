using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RowdyRod.EditorTools.AutoSave
{
    public class AutoSaveWindow : EditorWindow
    {
        private static string _saveLocation;
        private static List<string> _excludedScenes = new List<string>();
        private static List<string> _excludedAssets = new List<string>();

        [MenuItem("Rowdy Rod/Editor/Auto Save Preferences")]
        public static void ShowWindow()
        {
            GetWindow<AutoSavePreferences>("Auto Save Preferences");
            _saveLocation = EditorPrefs.GetString("AutoSave_SaveLocation", "");
            // Load excluded scenes and assets from EditorPrefs (implementation omitted for brevity)
        }

        private void OnGUI()
        {
            GUILayout.Label("Auto Save Preferences", EditorStyles.boldLabel);
            GuiLayout.Label("Made by RowdyRod", EditorStyles.miniLabel);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Save Location:", GUILayout.Width(100));
            Event evt = Event.current;
            Rect dropArea = GUILayoutUtility.GetLastRect();
            if (dropArea.Contains(evt.mousePosition) && evt.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                evt.Use();
            }
            else if (dropArea.Contains(evt.mousePosition) && evt.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();
                if (DragAndDrop.paths.Length > 0 && System.IO.Directory.Exists(DragAndDrop.paths[0]))
                {
                    _saveLocation = DragAndDrop.paths[0];
                    EditorPrefs.SetString("AutoSave_SaveLocation", _saveLocation);
                }
                evt.Use();
            }
            _saveLocation = EditorGUILayout.TextField(_saveLocation);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                _saveLocation = EditorUtility.OpenFolderPanel("Select Save Location", _saveLocation, "");
                EditorPrefs.SetString("AutoSave_SaveLocation", _saveLocation);
            }
            EditorGUILayout.EndHorizontal();

            // GUI for managing excluded scenes and assets (implementation omitted for brevity)

            if (GUI.changed)
            {
                // Save excluded scenes and assets to EditorPrefs (implementation omitted for brevity)
            }
        }
    }
}