using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotRandomizer : MonoBehaviour
{
    void Start()
    {
        //Randomize the start position of the dot
        Camera camera = Camera.main;
        Vector2 randomPosition = camera.ScreenToWorldPoint(new Vector2(
            Random.Range(50, Screen.width - 50),
            Random.Range(50, Screen.height - 50)
        ));

        transform.position = randomPosition;
    }
}
