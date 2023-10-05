using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    public int levelCount;
    public int levelTesting;
    public bool isLevelTesting;
    private void Start()
    {
        GetLevels();
    }
    public void GetLevels()
    {
        if(isLevelTesting)
        {
            levelCount = levelTesting;
        }
        else
        {
            levelCount = PlayerPrefs.GetInt("Levels");
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == levelCount)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }
    
}
