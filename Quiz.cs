using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions)]")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colours")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultButtonSprite;
    
    [Header("Timer")]
    
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("scoring")]
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    
    
    void Start()

    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();       
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);

        }
    }
    
    
    public void OnAnswerSelected(int index)
    
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

//Displaying whther we answer correctly

    void DisplayAnswer(int index)
    {
         Image buttonImage;

        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {
            correctAnswerIndex = currentQuestion
            .GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion
            .GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;/n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }


    }

    void GetNextQuestion()
    {
        if(questions.Count < 0)
        {
        SetButtonState(true);
        SetDefaultButtonSpitre();
        GetRandomQuestion();
        DisplayQuestion();
        scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()

    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
       
    }

    void DisplayQuestion()
    { 
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion
            .GetAnswer(i);
        }


    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        
        }
    }
    //For Loop
//int integer
// i = 0' ----------  'i' in this case is the button in the array of four that was set prior,
//        ----------  Equalling 0 being first button, ; end of condition? if i is less than <
//        ----------  answerButtons (the array) ?then run? length being the amount of buttons???
//        ----------  ;end of condition???? i++, add 1 and loop,

    void SetDefaultButtonSpitre()
    {
        

        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }
}
