using UnityEngine;
using UnityEngine.UI;

namespace Picker3D
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject[] panels;
        [SerializeField] private Image[] checkPointSquares;

        private int _howManyCheckPointCompleted = 0;

        private void Start()
        {
            GameManager.LevelFailed += GameManager_LevelFailed;
            GameManager.LevelCompleted += GameManager_LevelCompleted;
        }

        private void GameManager_LevelFailed()
        {

            DisabledSquares();
            ResetCheckPointSquareColor();
            FailUI();
        }

        private void GameManager_LevelCompleted()
        {

            DisabledSquares();
            ResetCheckPointSquareColor();
            WinUI();
        }

        public void BeforeStartUI() //0
        {
            TogglePanel(0);
            panels[0].SetActive(true);
        }
        public void InGameUI() //1
        {
            TogglePanel(1);
            EnableSquares(GameManager.Instance.NumberOfCheckpoints);
            panels[1].SetActive(true);
        }

        public void WinUI() //2
        {
            TogglePanel(2);
            panels[2].SetActive(true);


        }

        public void FailUI() //3
        {
            TogglePanel(3);
            panels[3].SetActive(true);
        }

        private void TogglePanel(int panelNumber)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (panelNumber == i) { }
                else
                    panels[i].SetActive(false);
            }
        }

        private void EnableSquares(int numberOfSquares)
        {
            for (int i = 0; i < numberOfSquares; i++)
            {
                checkPointSquares[i].enabled = true;
            }
        }

        private void ResetCheckPointSquareColor()
        {
            _howManyCheckPointCompleted = 0;
            foreach (var square in checkPointSquares)
            {
                square.color = Color.white;
            }
        }

        private void DisabledSquares()
        {
            for (int i = 0; i < 4; i++)
            {
                checkPointSquares[i].enabled = false;
            }
        }

        public void ChangeCheckPointSquareColor(Color color)
        {
            checkPointSquares[_howManyCheckPointCompleted].color = color;
            _howManyCheckPointCompleted++;
        }

        public void NextLevelButton()
        {
            GameManager.Instance.IsGameStarted = false;
            GameManager.Instance.ChangeGameState(GameManager.StateOfGame.BeforeStart);
            LevelManager.NextLevel();
        }

        public void TryAgainButton()
        {
            GameManager.Instance.IsGameStarted = false;
            GameManager.Instance.ChangeGameState(GameManager.StateOfGame.BeforeStart);
            LevelManager.RestartLevel();
        }

        public void StartGameButton()
        {
            GameManager.Instance.IsGameStarted = true;
            GameManager.Instance.ChangeGameState(GameManager.StateOfGame.InGame);
        }

    }
}

