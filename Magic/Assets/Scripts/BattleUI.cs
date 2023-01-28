using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] HealthControl enemyHealthControl, selfHealthControl;
    [SerializeField] private int selfMaxHealth, enemyMaxHealth, selfDamage, enemyDamage;
    [SerializeField] private Canvas quizCanvas, battleCanvas;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Text dialog;
    [SerializeField] private Image GameOverPanel,Conclusion;
    [SerializeField] private Sprite Win, Lose;

    private int enemyHealth, selfHealth;
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        selfHealth = selfMaxHealth;
        enemyHealthControl.SetMaxHealth(enemyMaxHealth);
        selfHealthControl.SetMaxHealth(selfMaxHealth);
    }

    public void BattleProcess(bool who)
    {
        
        openBattleUI();
        if (who)
        {
            //correct
            enemyHealth -= selfDamage;
            judgeHealth();
            dialog.text = "唉呀，別打我";
            Invoke("ReduceHealth", 1);
            if (quizManager.GameStatus != GameStatus.IDLE)
            {
                Invoke("openQuizUI", 2.5f);
            }
            else
            {
                Conclusion.sprite = Win;
                Invoke("OpenGameOverPanel", 2.5f);
            }
        }
        else
        {
            //wrong
            selfHealth-= enemyDamage;
            judgeHealth();
            dialog.text = "吃我一頓粗飽";
            Invoke("ReduceHealth", 1);
            if (quizManager.GameStatus != GameStatus.IDLE)
            {
                Invoke("openQuizUI", 2.5f);
            }
            else
            {
                Conclusion.sprite = Lose;
                Invoke("OpenGameOverPanel", 2.5f);
            }
        }
    }
    
    void OpenGameOverPanel()
    {
        GameOverPanel.transform.gameObject.SetActive(true);
    }
    void ReduceHealth()
    {
        enemyHealthControl.SetHealth(enemyHealth);
        selfHealthControl.SetHealth(selfHealth);
    }

    void openBattleUI()
    {
        battleCanvas.transform.gameObject.SetActive(true);
        quizCanvas.transform.gameObject.SetActive(false);
    }

    public void StartStatus()
    {
        battleCanvas.transform.gameObject.SetActive(true);
        dialog.text = "挑戰我???";
    }
    void judgeHealth()
    {
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            quizManager.GameStatus = GameStatus.IDLE;
        }
        if (selfHealth <= 0)
        {
            selfHealth = 0;
            quizManager.GameStatus = GameStatus.IDLE;
        }
    }

    public void openQuizUI()
    {
        battleCanvas.transform.gameObject.SetActive(false);
        quizCanvas.transform.gameObject.SetActive(true);
        quizManager.GameStatus = GameStatus.ANSWER;
    }
}
