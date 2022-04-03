using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector] public int score;


    void Start()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log(score);
    }


}
