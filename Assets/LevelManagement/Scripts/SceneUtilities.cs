using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;

public static class SceneUtilities
{
    [SerializeField]
    private static int _mainMenuIndex = 1;

    public static void LoadLevel(string newLevel) {
        if (Application.CanStreamedLevelBeLoaded(newLevel)) {
            SceneManager.LoadScene(newLevel);
        } else {
            Debug.LogWarning("SceneUtilities: Load LoadLevel Error: invalid scene specified");
        }
    }

    public static void LoadLevel (int levelIndex) {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings) {
            if (levelIndex == _mainMenuIndex) {
                MainMenu.Open();
            }
            SceneManager.LoadScene(levelIndex);
        } else {
            Debug.LogWarning("SceneUtilities: Load LoadLevel Error: invalid scene specified");
        }
    }


    public static void LoadNextLevel() {
        int nexSceneIndex = ( SceneManager.GetActiveScene().buildIndex + 1 ) % SceneManager.sceneCountInBuildSettings;
        nexSceneIndex = Mathf.Clamp(nexSceneIndex, _mainMenuIndex, nexSceneIndex);
        LoadLevel(nexSceneIndex);
    }

    public static void ReloadLevel() {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadMainMenuLevel() {
        MainMenu.Open();
        SceneManager.LoadScene(_mainMenuIndex);
    }
}
