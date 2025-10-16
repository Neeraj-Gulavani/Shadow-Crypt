using UnityEditor;
using UnityEngine;

public static class ClearPlayerPrefsEditor
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("✅ PlayerPrefs cleared!");
    }
}

