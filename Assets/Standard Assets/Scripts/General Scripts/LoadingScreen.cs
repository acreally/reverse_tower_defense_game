using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
    // GUI style for the background
    public GUIStyle backgroundStyle;

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "LOADING",
            backgroundStyle);
    }
}
