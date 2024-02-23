using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> fishInfos;
    private int currentIndex;
     
    public void MoveToNextFish()
    {
        if(currentIndex < fishInfos.Count - 1)
        {
            fishInfos[currentIndex].SetActive(false);
            currentIndex++;
            fishInfos[currentIndex].SetActive(true);
        }
        else
        {
            fishInfos[currentIndex].SetActive(false);
            currentIndex = 0;
            fishInfos[currentIndex].SetActive(true);
        }
    }

    public void MoveToPrevFish()
    {
        if (currentIndex > 0)
        {
            fishInfos[currentIndex].SetActive(false);
            currentIndex--;
            fishInfos[currentIndex].SetActive(true);
        }
        else 
        {
            fishInfos[currentIndex].SetActive(false);
            currentIndex = fishInfos.Count - 1;
            fishInfos[currentIndex].SetActive(true);
        }
    }

}
