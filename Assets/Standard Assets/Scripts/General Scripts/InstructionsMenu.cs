using UnityEngine;
using System.Collections;

public class InstructionsMenu : MonoBehaviour {
    // GUI style for the background
    public GUIStyle backgroundStyle;
    // GUI style for the back button
    public GUIStyle backButtonStyle;

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", 
            backgroundStyle);
        if (GUI.Button(new Rect(
            Screen.width - 80, Screen.height - 80, 64, 64), "", 
            backButtonStyle))
        {
            // Return to the title screen
            Application.LoadLevel(0);
        }
    }
}
