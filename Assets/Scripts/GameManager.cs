using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 2f;
    public float titleDelay = 2f;
    public Text death;
    public Text title;

    public bool gameHasEnded = false;

    void Start()
    {
            title.text = "Next Try";
        Invoke("clearTitle", restartDelay);
    }

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

    void clearTitle()
    {
        title.text = "";
    }
}
