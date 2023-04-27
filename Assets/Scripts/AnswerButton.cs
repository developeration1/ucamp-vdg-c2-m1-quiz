using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public bool isRight;
    public float waitTime;

    private Image image;
    private Button buttonComponent;

    public Button ButtonComponent
    {
        get
        {
            return buttonComponent;
        }
    }

    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        image = GetComponent<Image>();
        buttonComponent = GetComponent<Button>();
    }
    public void PushButton()
    {
        if(isRight)
        {
            gameManager.AddScore();
        }
        StartCoroutine(BlockAnswersForTimeRoutine());
        StartCoroutine(NextQuestionRoutine());
    }

    private IEnumerator NextQuestionRoutine()
    {
        yield return PaintAnswerRoutine();
        gameManager.NextQuestion();
    }

    public IEnumerator PaintAnswerRoutine()
    {
        if (isRight)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
            gameManager.PaintCorrectAnswers();
        }
        yield return new WaitForSeconds(waitTime);

        image.color = Color.white;
    }

    public IEnumerator BlockAnswersForTimeRoutine()
    {
        foreach (AnswerButton answer in gameManager.answerButtons)
        {
            answer.ButtonComponent.interactable = false;
        }
        yield return new WaitForSeconds(waitTime);
        foreach (AnswerButton answer in gameManager.answerButtons)
        {
            answer.ButtonComponent.interactable = true;
        }
    }
}
