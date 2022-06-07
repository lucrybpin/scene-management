using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;

public class GameManager : MonoBehaviour
{

    //reference to objective
    private Objective _objective;

    private bool _isGameOver;
    public bool IsGameOver { get { return _isGameOver; } }

    private bool _ended = false;


    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private TransitionFader _endTransitionPrefab;

    private void Awake () {
        _objective = Object.FindObjectOfType<Objective>();

        if (_instance != null) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }

    public void EndLevel () {
        _isGameOver = true;
        _ended = true;
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine() {
        TransitionFader.PlayTransition(_endTransitionPrefab);
        float fadeDelay = ( _endTransitionPrefab != null ) ? 
            _endTransitionPrefab.Delay + _endTransitionPrefab.FadeOnDuration : 0;
        yield return new WaitForSeconds(fadeDelay);
        WinMenu.Open();
    }

    private void Update () {
        if (_objective != null && _objective.IsComplete && _ended == false) {
            EndLevel();
        }
    }

}
