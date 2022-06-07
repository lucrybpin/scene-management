using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteObjectiveAfterTime : MonoBehaviour
{
    float elapsedTime = 0;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 5f) {
            if (GetComponent<Objective>().IsComplete == false) {
                GetComponent<Objective>().CompleteObjective();
            }
        }
            
    }
}
