///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

    void Awake()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2);
        StartGame();
    }

    public void StartGame()
    {
        //TriggerEvent
        EventManager.GameStartTrigger();
    }
	
    public void GameOver()
    {
        //TriggerEvent
        EventManager.GameOverTrigger();
    }
	
}
