using UnityEditor;
using UnityEngine;

namespace RowdyRod.EditorTools.AutoSave
{
    public class AutoSavePreferences : EditorWindow
    {
        [PreferenceItem("Auto Save (AutoSave)")]
        public static void PreferencesGUI()
        {
            // Auto-save enable/disable
            Config.AutoSaveConfig.Enabled.value = EditorGUILayout.Toggle(
                "Enable Auto Save", 
                Config.AutoSaveConfig.Enabled.value
            );

            // Save interval
            Config.AutoSaveConfig.Delay.value = EditorGUILayout.FloatField(
                "Save Interval (seconds)", 
                Config.AutoSaveConfig.Delay.value
            );

            // Save preferences
            if (GUI.changed)
            {
                EditorPrefs.SetBool("AutoSave_Enabled", Config.AutoSaveConfig.Enabled.value);
                EditorPrefs.SetFloat("AutoSave_Delay", Config.AutoSaveConfig.Delay.value);
                
                // Reinitialize the auto-save utility with new settings
                AutoSaveUtil.Initialize();
            }
        }
    }
}