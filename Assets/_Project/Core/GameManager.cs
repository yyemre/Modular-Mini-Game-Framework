﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadMiniGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}