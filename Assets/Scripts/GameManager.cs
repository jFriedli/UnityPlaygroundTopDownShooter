using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 2f;
    public Text death;

    public bool gameHasEnded = false;
    public void gameOver()
    {
        if(!gameHasEnded)
        {
            gameHasEnded = true;
            death.text = "YOU DED LMAO";
            Invoke("restart", restartDelay);
        }
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
