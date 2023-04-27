using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int correctAnswerValue = 100;
    public int score = 0;
    public float timeToNextQuestion;
    public TMP_Text scoreText;
    public Question[] questions;
    public TMP_Text questionText;
    public TMP_Text[] answersText;
    public AnswerButton[] answerButtons;
    private AnswerButton[] correctAnswerButtons;
    private int questionCounter = 0;

    private void Start()
    {
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (questionCounter < questions.Length)
        {
            int randomQuestionIndex = SelectRandomUnansweredQuestionIndex();
            MixAnswers(randomQuestionIndex);
            questionText.text = questions[randomQuestionIndex].question;
            correctAnswerButtons = new AnswerButton[answerButtons.Length];

            for (int i = 0; i < answersText.Length; i++)
            {
                answersText[i].text = questions[randomQuestionIndex].answers[i].text;
                answerButtons[i].isRight = questions[randomQuestionIndex].answers[i].isRight;
                if (answerButtons[i].isRight == true)
                {
                    correctAnswerButtons[i] = answerButtons[i];
                }
                else
                {
                    correctAnswerButtons[i] = null;
                }
            }
            questionCounter++; // questionCounter = questionCounter + 1;
        }
        else
        {
            SceneLoader sl = FindObjectOfType<SceneLoader>();
            sl.LoadScene("GameOver");
        }
    }

    public void AddScore()
    {
        score += correctAnswerValue;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public void PaintCorrectAnswers()
    {
        foreach (AnswerButton button in correctAnswerButtons)
        {
            if (button != null)
            {
                StartCoroutine(button.PaintAnswerRoutine());
            }
        }
    }

    public int SelectRandomUnansweredQuestionIndex()
    {
        int index = -1;
        while(index == -1)
        {
            int randomIndex = Random.Range(0, questions.Length);
            if (!questions[randomIndex].answered)
            {
                index = randomIndex;
                questions[randomIndex].answered = true;
            }
        }
        return index;
    }

    private void MixAnswers(int questionIndex)
    {
        Answer[] mixedAnswers = new Answer[questions[questionIndex].answers.Length];
        for(int i = 0; i < mixedAnswers.Length; i++)
        {
            bool pass = false;
            while (pass == false)
            {
                int randomIndex = Random.Range(0, mixedAnswers.Length);
                if (mixedAnswers[randomIndex] == null)
                {
                    mixedAnswers[randomIndex] = questions[questionIndex].answers[i];
                    pass = true;
                }
            }
        }
        questions[questionIndex].answers = mixedAnswers;
    }
}
