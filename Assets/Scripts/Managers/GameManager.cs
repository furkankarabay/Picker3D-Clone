using System;
using UnityEngine;

using Picker3D.Platform.CheckPointSystem;

namespace Picker3D
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private LevelList levelList;
        public bool IsGameStarted = false;
        public int NumberOfCheckpoints;
        public static event Action LevelCompleted = delegate { };
        public static event Action LevelFailed = delegate { };
        public enum StateOfGame
        {
            BeforeStart,
            InGame,
            Fail,
            Win
        }

        StateOfGame state;

        void Start()
        {
            CheckPoint.Completed += CheckPoint_Completed;
            state = StateOfGame.BeforeStart;

            LevelManager.SetLevelManager(levelList);
        }

        private void CheckPoint_Completed()
        {
            UIManager.Instance.ChangeCheckPointSquareColor(Color.green);
        }

        void Update()
        {
            NumberOfCheckpoints = LevelManager.currentLevel.numberOfCheckPoint;
            
            switch (state)
            {
                case StateOfGame.BeforeStart:
                    UIManager.Instance.BeforeStartUI();
                    break;
                case StateOfGame.InGame:
                    UIManager.Instance.InGameUI();
                    break;
                case StateOfGame.Fail:
                    LevelFailed();
                    break;
                case StateOfGame.Win:
                    LevelCompleted();
                    break;
                default:
                    break;
            }
        }

        public void ChangeGameState(StateOfGame state)
        {
            Debug.Log(state);
            this.state = state;
        }
    }

}
