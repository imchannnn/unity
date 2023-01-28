using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Image questionImage;
    [SerializeField] private Text questionText, timerText;
    [SerializeField] private List<Button> options;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Canvas quizUI;
    [SerializeField] private Canvas battleUI;

    private Question question;
    private bool answered;

    public Text TimerText { get { return timerText; } }
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuesiton(Question question)
    {
        this.question = question;
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                questionImage.transform.parent.gameObject.SetActive(true);
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
        }
        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
        }
        answered = false;
    }

    private void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.ANSWER)
        {
            if (!answered)
            {
                answered = true;
                bool val = quizManager.Answer(btn.name);
            }
        }
    }
}
