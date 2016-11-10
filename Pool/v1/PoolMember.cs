///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;
using System.Collections;

public class PoolMember : MonoBehaviour {

    //TODO Write some elegant stuff here.

    public bool InPool;

    [ContextMenuItem("Return to Pool", "ReturnPool")]

    public Pool MyPool;

    //private void returnPoolContextFunction()
    //{
    //    ReturnPool();
    //}

    void OnEnable()
    {
        EventManager.GameOverEvent += OnGameOver;
    }

    void OnDisable()
    {
        EventManager.GameOverEvent -= OnGameOver;
    }

    public void OnPoolEnter()
    {
        transform.parent = MyPool.transform;
        //StartCoroutine(ReturnToPapaPool());
        
        gameObject.SetActive(false);
        //EventManager.GameOverEvent -= ReturnPool;

        InPool = true;
    }

    public void OnPoolOut()
    {
        transform.parent = null;
        gameObject.SetActive(true);

        //Return to pool on GameOver
        //EventManager.GameOverEvent += ReturnPool;

        InPool = false;
    }


    public void ReturnPool()
    {
        MyPool.ReturnOld(gameObject);
    }

    void OnGameOver()
    {
        //Debug.Log("On Game Over");

        if (!InPool)
            ReturnPool();

    }


}
