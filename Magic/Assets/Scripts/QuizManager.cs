using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizDataScriptable quizData;
    [SerializeField] private float timeLimit;
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private BattleUI battleUI;
    [SerializeField] private Canvas menu,QuizCanvas;



    private List<Question> questions;
    private Question selectedQuestion;
    private float currentTimer;

    private GameStatus gameStatus = GameStatus.IDLE;
    public GameStatus GameStatus
    {
        get { return gameStatus; }
        set { gameStatus = value; }
    }
    public void StartGame()
    {
        menu.transform.gameObject.SetActive(false);
        battleUI.StartStatus();
        Invoke("StartGame2", 1.5f);
    }

    void StartGame2()
    {
        QuizCanvas.transform.gameObject.SetActive(true);
        currentTimer = timeLimit;
        questions = new List<Question>();
        for (int i = 0; i < quizData.questions.Count; i++)
        {
            questions.Add(quizData.questions[i]);
        }
        gameStatus = GameStatus.ANSWER;
        SelectQuestion();
    }

    void Update()
    {
        if (gameStatus == GameStatus.ANSWER)
        {
            currentTimer -= Time.deltaTime;
            SetTimer(currentTimer);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time:" + time.ToString("mm':'ss");
        if (currentTimer <= 0)
        {
            gameStatus = GameStatus.BATTLE;
            battleUI.BattleProcess(false);
            TimeReset();
            Invoke("SelectQuestion", 0.4f);
        }
    }

    public void TimeReset()
    {
        currentTimer = timeLimit;
    }

    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        quizUI.SetQuesiton(selectedQuestion);
        questions.RemoveAt(val);
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;
        gameStatus = GameStatus.BATTLE;
        TimeReset();
        if (answered == selectedQuestion.correctAns)
        {
            //YES
            correctAns = true;
            battleUI.BattleProcess(true);
        }
        else
        {
            battleUI.BattleProcess(false);
        }

        if (questions.Count > 0)
        {
            Invoke("SelectQuestion", 0.4f);
        }
        else
        {
            gameStatus = GameStatus.IDLE;
            //結束遊戲
            //*******
            //結束遊戲
        }
        return correctAns;
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public List<string> options;
    public string correctAns;
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
}

public enum GameStatus
{
    IDLE,
    ANSWER,
    BATTLE
}