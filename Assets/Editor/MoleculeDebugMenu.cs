using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Various debug utilities to help out when a VR headset is not available.
public class MoleculeDebugMenu : EditorWindow
{
    [MenuItem("Molecule/Tools/DebugMenu")]
    private static void NewNestedOption()
    {
        EditorWindow.GetWindow(typeof(MoleculeDebugMenu));
    }

    public void OnGUI()
    {
        using (new EditorGUI.DisabledScope(isEditorInEditMode()))
        {
            renderControllerButtons();
        }
    }

    private void renderControllerButtons()
    {
        var inputManager = GameObject.Find("/InputManager")?.GetComponent<InputManager>();
        var gameStateManager = GameObject.Find("/GameManager")?.GetComponent<GameStateManager>();
        if(inputManager == null) return;

        if (GUILayout.Button("Squeeze Right")) {
            inputManager.RightSqueezeFired();
        }
        if (GUILayout.Button("Squeeze Left")) {
            inputManager.LeftSqueezeFired();
        }
        if (GUILayout.Button("Right Trigger")) {
            inputManager.RightTriggerFired();
        }
        if (GUILayout.Button("Left Trigger")) {
            inputManager.LeftTriggerFired();
        }
        if (GUILayout.Button("Pause")) {
            gameStateManager.PauseGame();
        }
        if (GUILayout.Button("Unpause")) {
            gameStateManager.UnpauseGame();
        }
    }

    private bool isEditorInEditMode()
    {
        if(EditorApplication.isPlaying) return false;
        if(EditorApplication.isPaused)  return false;

        return true;
    }
}

