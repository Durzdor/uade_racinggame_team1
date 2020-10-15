using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private bool isPaused;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h !=0 || v != 0)
        {
            _player.Move(new Vector3(h, 0, v));
        }
    }
}
