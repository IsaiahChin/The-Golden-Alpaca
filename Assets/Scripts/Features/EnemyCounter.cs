using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCounter : MonoBehaviour
{
    //UI text object
    private Text enemyCounterText;
    //Counter paramaters
    private int enemyCounter;
    private int maxEnemies;
    private int minEnemies;

    /**
    * This sets the UI object and sets the initial counter properties/limits
    */
    void Start()
    {
        enemyCounterText = this.GetComponent<Text>();
        enemyCounter = 0;
        maxEnemies = 100;
        minEnemies = 0;
    }

    /**
    * This method increases the enemy counter up to the upper limit of maxEnemies
    */
    public void increaseCount()
    {
        if (enemyCounter >= maxEnemies)
        { enemyCounter = maxEnemies; }
        else
        { enemyCounter += 1; }
    }

    /**
    * This method increases the enemy counter down to the lower limit of minEnemies
    */
    public void decreaseCount()
    {
        if (enemyCounter <= minEnemies)
        { enemyCounter = minEnemies; }
        else
        { enemyCounter -= 1; }
    }

    /**
    * This method updates the UI text to the current enemy count
    */
    void Update()
    {
        enemyCounterText.text = enemyCounter.ToString();
    }
}
