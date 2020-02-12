using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostTextDisplayer : MonoBehaviour
{
    Text text;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Boost: " + player.GetComponent<PlayerHolder>().boostCurrent.ToString();
    }
}
