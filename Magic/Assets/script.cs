using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script : MonoBehaviour
{
    [SerializeField] QuizManager quizManager;
    [SerializeField] BattleUI battleUI;
    [SerializeField] Canvas quizCanvas,BattleCanvas;
    [SerializeField] Image Conclusion,GameOverPanel;
    [SerializeField] Sprite Lose,Win;
    private void result(bool res)
    {
        if (res)
        {
            if(quizManager.GameStatus != GameStatus.IDLE)
            {
                quizCanvas.transform.gameObject.SetActive(false);
                BattleCanvas.transform.gameObject.SetActive(true);
                quizManager.GameStatus = GameStatus.ANSWER;
            }
            else
            {
                Conclusion.sprite = Win;
                GameOverPanel.transform.gameObject.SetActive(true);                
            }
        }
        else
        {
            
        }
    }
}
