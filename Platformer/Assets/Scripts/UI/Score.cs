using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector] public int score;

    GameObject userInterface;

    void Start()
    {
        score = 0;
        userInterface = GameObject.Find("Score");
    }

    public void AddScore(int points)
    {
        score += points;
        userInterface.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        Debug.Log(score);
    }


}
