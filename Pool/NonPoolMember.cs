using UnityEngine;
using System.Collections;

public class NonPoolMember : MonoBehaviour {
	
    void OnEnable()
    {
        EventManager.GameOverEvent += OnGameOver;
    }

    void OnDisable()
    {
        EventManager.GameOverEvent -= OnGameOver;
    }

    public void Begone()
    {

        //EventManager.GameOverEvent -= OnGameOver;

        Destroy(gameObject);
    }

    void OnGameOver()
    {

        //Debug.Log("GameIS Over for Non Pool Member");

        //Destrtoy this madness when game over
        //EventManager.GameOverEvent -= OnGameOver;

        ////destroy Recursively
        //GameObject[] gos = new GameObject[transform.childCount];
        //
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    gos[i] = transform.GetChild(i).gameObject;
        //}
        //
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Destroy(gos[i]);
        //}

        PoolManager.Instance.ReturnObject(this.gameObject);
        //GetComponent<MovingObjectBehaviour>().ReturnObject();

    }


}

