using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager> {
    
    public Button RestartButton;

    void OnEnable()
    {
        Debug.Log("Subscribed");
        EventManager.GameStartEvent += OnGameStart;
        EventManager.GameOverEvent += OnGameOver;
    }

    void OnDisable()
    {
        Debug.Log("UnSubscribed");
        EventManager.GameStartEvent -= OnGameStart;
        EventManager.GameOverEvent -= OnGameOver;
    }
    
    void OnGameOver()
    {
        RestartButton.gameObject.SetActive(true);
        RestartButton.onClick.AddListener(RestartGame);
    }

    void OnGameStart()
    {
        RestartButton.gameObject.SetActive(false);
        RestartButton.onClick.RemoveListener(RestartGame);
    }

    void RestartGame()
    {
        GameManager.Instance.StartGame();
    }
    
}
