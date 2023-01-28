using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCon : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] BattleUI battleUI;
    GameObject rOut;
    GameObject rIn;
    ParticleSystem inPar;
    ParticleSystem outPar;
    // Start is called before the first frame update
    void Start()
    {
        rOut = game.transform.GetChild(0).gameObject;
        rIn = game.transform.GetChild(1).gameObject;
        outPar = rOut.GetComponent<ParticleSystem>();
        inPar = rIn.GetComponent<ParticleSystem>();
        game.SetActive(false);
    }

    private void rotate()
    {
        game.SetActive(true);
        if (inPar.isPlaying && outPar.isPlaying)
        {
            print("All play");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            print("trigger success");
        }
        rotate();
    }
}
