using LevelManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class WinMenu :Menu<WinMenu>
    {

        public void OnNextLevelPressed() {
            base.OnBackPressed();
            SceneUtilities.LoadNextLevel();
        }

        public void OnRestartPressed() {
            base.OnBackPressed();
            SceneUtilities.ReloadLevel();
        }

        public void OnMainMenuPressed() {
            base.OnBackPressed();
            SceneUtilities.LoadMainMenuLevel();
        }
    } 
}
