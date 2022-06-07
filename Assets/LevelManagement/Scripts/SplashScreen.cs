using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    [RequireComponent(typeof(ScreenFader))]
    public class SplashScreen :MonoBehaviour
    {
        [SerializeField]
        private ScreenFader _screnFader;

        [SerializeField]
        private float delay;

        private void Awake () {
            _screnFader = GetComponent<ScreenFader>();
        }

        private void Start () {
            _screnFader.FadeOn();
        }

        public void FadeAndLoad() {
            StartCoroutine(FadeAndLoadRoutine());
        }

        private IEnumerator FadeAndLoadRoutine() {
            yield return new WaitForSeconds(delay);
            _screnFader.FadeOff();
            SceneUtilities.LoadMainMenuLevel();

            yield return new WaitForSeconds(_screnFader.FadeOffDuration);

            Object.Destroy(gameObject);
        }
    } 
}
