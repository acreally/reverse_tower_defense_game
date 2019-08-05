using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {
    // GUI Style for the background
    public GUIStyle backgroundStyle;
    // GUI Style for the play button
    public GUIStyle playButtonStyle;
    // GUI Style for the instructions button
    public GUIStyle instructionButtonStyle;

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "",
            backgroundStyle);
        if (GUI.Button(new Rect(
            Screen.width - 636, Screen.height - 304, 256, 96), "",
            playButtonStyle))
        {
            Application.LoadLevel("loading");
        }
        if (GUI.Button(new Rect(
            Screen.width - 636, Screen.height - 192, 256, 96), "",
            instructionButtonStyle))
        {
            // Go to the instructions screen
            Application.LoadLevel("instructions");
        }
    }
}
