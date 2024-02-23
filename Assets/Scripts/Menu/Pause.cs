using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pannel;
    public void OnPause(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow) )
            {
                GetComponent<IndexMenu>().MoveToPrevFish();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GetComponent<IndexMenu>().MoveToNextFish();
            }

        }
    }
}
