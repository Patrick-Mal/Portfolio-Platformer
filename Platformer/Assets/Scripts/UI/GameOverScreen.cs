using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
