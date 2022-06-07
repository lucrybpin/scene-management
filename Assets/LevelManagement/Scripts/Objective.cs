using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    private bool _isComplete;
    public bool IsComplete { get { return _isComplete; } }

    public void CompleteObjective() {
        _isComplete = true;
    }
}
