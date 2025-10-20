#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class PlayerPrefsUtility
{
    [MenuItem("Tools/Clear PlayerPrefs %#r")] // Ctrl+Shift+R
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("✅ PlayerPrefs cleared!");
    }
}
#endif
