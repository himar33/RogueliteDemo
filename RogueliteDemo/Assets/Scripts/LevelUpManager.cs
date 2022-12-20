using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public float currentLevel;
    public float nextLevelXp;
    public float baseLevelAdd;

    private void Start()
    {
        currentLevel = 1;
        nextLevelXp = currentLevel * baseLevelAdd;
    }

    public void GetXP(float amount)
    {
        nextLevelXp -= amount;
        if (nextLevelXp <= 0)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        nextLevelXp = currentLevel * baseLevelAdd;
    }
}
