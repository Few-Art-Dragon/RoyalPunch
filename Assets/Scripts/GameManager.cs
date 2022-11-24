using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent<StateGame> SetGameStateEvent;
    public enum StateGame
    {
        MainMenu,
        Pause,
        Play


    } 
    public StateGame stateGame;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;


    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _pauseMenu;

    private void SetParam()
    {
        
    }

    void Awake()
    {
        SetGameStateEvent = new UnityEvent<StateGame>();
        SetGameStateEvent.AddListener(SetGameState);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGameState(StateGame stateGame)
    {
        switch (stateGame)
        {
            case StateGame.MainMenu:
                break;
            case StateGame.Play:
                DisableMainMenu();
                EnableScriptsPlayer();
                EnableEnemy();
                break;     
           case StateGame.Pause:
                EnablePauseMenu();
                break;
                
            
        }
        stateGame = StateGame.Play;
        
    }

    private void EnableScriptsPlayer()
    {
        _player.GetComponent<Player>().enabled = true;
        _player.GetComponent<MovePlayer>().enabled = true;
        _player.GetComponent<LookAt>().enabled = true;
    }
    private void DisableScriptsPlayer()
    {
        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<MovePlayer>().enabled = false;
        _player.GetComponent<LookAt>().enabled = false;
    }

    private void EnableEnemy()
    {
        _enemy.SetActive(true);
    }

    private void DisableMainMenu()
    {
        _mainMenu.SetActive(false);
    }
    private void EnableMainMenu()
    {
        _mainMenu.SetActive(true);
    }


    private void EnablePauseMenu()
    {
        _pauseMenu.SetActive(true);
    }

}
