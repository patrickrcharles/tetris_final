using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateScore_single : MonoBehaviour {

    public int scoreOneLine = 50;
    public int scoreTwoLines = 100;
    public int scoreThreeLines = 200;
    public int scoreFourLines = 400;
    public int scoreFiveLines = 800;
    public int scoreSixLines = 1600;
    public int scoreSevenLines = 3200;
    public int scoreEightLines = 6400;
    public int numberOfRowsThisTurn = 0;
    public int currentScore = 0;
    public int numLines = 0;
    public TextMesh Hud_score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void updateCurrentScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                clearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                clearedTwoLines();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                clearedThreeLines();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                clearedFourLines();
            }
            else if (numberOfRowsThisTurn == 5)
            {
                clearedFiveLines();
            }
            else if (numberOfRowsThisTurn == 6)
            {
                clearedSixLines();
            }
            else if (numberOfRowsThisTurn == 7)
            {
                clearedSevenLines();
            }
             else if (numberOfRowsThisTurn == 8)
            {
                clearedEightLines();
            }

            numberOfRowsThisTurn = 0;
        }
    }

    public void UpdateUI()
    {
        Hud_score.text = "score : " + currentScore.ToString() + "\n" + "lines  : " + numLines.ToString();
    }

    public void clearedOneLine()
    {
        currentScore += scoreOneLine;
        numLines += 1;

    }

    public void clearedTwoLines()
    {
        currentScore += scoreTwoLines;
        numLines += 2;
    }

    public void clearedThreeLines()
    {
        currentScore += scoreThreeLines;
        numLines += 3;
    }

    public void clearedFourLines()
    {
        currentScore += scoreFourLines;
        numLines += 4;

    }

    public void clearedFiveLines()
    {
        currentScore += scoreFiveLines;
        numLines += 5;
    }

    public void clearedSixLines()
    {
        currentScore += scoreSixLines;
        numLines += 6;
    }

    public void clearedSevenLines()
    {
        currentScore += scoreSevenLines;
        numLines += 7;

    }
    public void clearedEightLines()
    {
        currentScore += scoreEightLines;
        numLines += 8;
    }
}
