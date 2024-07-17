using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswers;
  
    } 
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;

    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100); 
    

    }
     //this would intiger division will reurn zero we must cast an int variable into a float
    //(float) does this
    //this will lead to error as we are looking for int 
    //Mathf,round rounds the float to readable data

    //now hook to qzscrpt
}