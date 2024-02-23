using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject pannel;
    [SerializeField] List<GameObject> buttonsToDesactivate;
    bool isactive;
    public void OpenIndex()
    { 
        foreach(GameObject button in buttonsToDesactivate)
        {
            button.SetActive(false);
        }

        //pannel.SetActive(true);
    }

    public void CloseIndex()
    {
        foreach (GameObject button in buttonsToDesactivate)
        {
            button.SetActive(true);
        }

        //pannel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isactive = !isactive;
            pannel.SetActive(isactive);
        }
            if (buttonsToDesactivate.Count > 0)
        {
            if (Time.timeScale == 0)
            {
                OpenIndex();
            }
            else if (Time.timeScale > 0 && pannel.activeInHierarchy)
            {
                CloseIndex();
            }
        }

        

    }
}
