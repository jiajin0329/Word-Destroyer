using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class LevelMenu : ILevelMenu
    {
        [SerializeField]
        private GameObject _levelEndUi;
        [SerializeField]
        private GameObject _diedText;
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private Button _restartButton;

        public void ReSet()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        public void CloseLevelEndUI() => _levelEndUi.SetActive(false);

        public void OpenDiedUI()
        {
            _levelEndUi.SetActive(true);
            _diedText.SetActive(true);
            _winText.SetActive(false);
        }

        public void OpenWinUI()
        {
            _levelEndUi.SetActive(true);
            _winText.SetActive(true);
            _diedText.SetActive(false);
        }

        public void AddRestartButtonListener(UnityAction _unityAction) => _restartButton.onClick.AddListener(_unityAction);
    }
}