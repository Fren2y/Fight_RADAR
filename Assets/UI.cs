using UnityEngine;

namespace Fight_RADAR
{
    public class UI : MonoBehaviour
    {
        [SerializeField] GameObject _mainMenu;
        [SerializeField] GameObject _gameMenu;
        [SerializeField] GameOverWin _gameOver;

        private void Start()
        {
            Game.inst.e_startGame.AddListener(StartGame);
            Game.inst.e_teamWin.AddListener(_gameOver.ShowInfo);
        }

        private void OnDestroy()
        {
            Game.inst.e_startGame.RemoveListener(StartGame);
            Game.inst.e_teamWin.RemoveListener(_gameOver.ShowInfo);
        }

        private void StartGame(bool value)
        {
            _mainMenu.SetActive(!value);
            _gameMenu.SetActive(value);
        }

        #region Btns

        public void PlayBtn()
        {
            Game.inst.StartGame(true);
        }

        public void RestartBtn()
        {
            Game.inst.StartGame(false);
            Game.inst.StartGame(true);
        }

        #endregion
    }
}
