using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public enum GameOverType { Win, Lose };

    [SerializeField] GameObject mainPlayer;
    [SerializeField] PlayerController playerController;
    [SerializeField] EnemyController enemyController;
    [SerializeField] CameraController mainCamera;
    UIManager UIManager;
    bool isGameOver;

    private void Awake()
    {
        UIManager = GetComponent<UIManager>();
    }
    public void StartGame()
    {
        Debug.Log("Oyun Baþladý");
        UIManager.ActiveJoystick();
        UIManager.ScrollDown();
        //enemyController.StartEnemyCreation();
        mainCamera.ZoomOut();
        isGameOver = false;
    }
    public void GameOver(GameOverType gameOverType)
    {
        GameObject[] gameObjects;
        if (!isGameOver)
        {
            isGameOver = true;
            mainPlayer.GetComponent<MainPlayerMovement>().StopMovement();
            if (gameOverType == GameOverType.Win)
            {
                gameObjects = playerController.GetPlayers();
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.GetComponent<PlayerMovement>().StopMovement();
                    gameObject.transform.LookAt(Vector3.back);
                    gameObject.GetComponent<Animator>().SetTrigger("Dance");
                }
            }
            else
            {
                gameObjects = enemyController.GetEnemys();
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Dance");
                }
            }
            mainCamera.ZoomIn();
            UIManager.DisableJoystick();
            enemyController.StopEnemyCreation();
            // TODO: - Oyun sonu sahnesi (kazandýðý para falan)
        }
    }
}
