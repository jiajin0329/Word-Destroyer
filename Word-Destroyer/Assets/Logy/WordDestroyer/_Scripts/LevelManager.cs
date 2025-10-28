using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class LevelManager
    {
        private CancellationTokenSource _cancellationTokenSource;
        [SerializeField]
        private LevelDatas _levelDatas;
        [SerializeField]
        private WordGenerator _wordGenerator = new();
        private WordAttacker _wordAttacker;
        [SerializeField]
        private LevelMenu _levelMenu = new();
        private ILevelMenu _iLevelMenu;
        [SerializeField]
        private LevelStartUI _levelStartUI;

        public LevelManager(LevelDatas _levelDatas, WordAttacker _wordAttacker, ILevelMenu _iLevelMenu, LevelStartUI _levelStartUI)
        {
            this._levelDatas = _levelDatas;
            this._wordAttacker = _wordAttacker;
            this._iLevelMenu = _iLevelMenu;
            this._levelStartUI = _levelStartUI;
        }

        public void Initialize()
        {
            _levelDatas.Initialize();
            _wordGenerator.Initialize(_levelDatas);
            _wordAttacker.Initialize(_levelDatas);

            AddListener();
        }

        private void AddListener()
        {
            _levelDatas.playerDatas.AddLoseHpEventListener(CheckLevelFailed);
            _levelDatas.AddSetStateListener(LevelFailed);

            _levelDatas.wordGeneratorDatas.AddRemoveWordListener(CheckLevelWin);
            _levelDatas.AddSetStateListener(LevelWin);

            if (_iLevelMenu == null)
                _iLevelMenu = _levelMenu;
            _iLevelMenu.AddRestartButtonListener(Restart);

        }

        private void CheckLevelFailed(int _hp)
        {
            if (_hp <= 0)
                _levelDatas.SetState(LevelDatas.State.levelFailed);
        }

        private void LevelFailed(LevelDatas.State _state)
        {
            if (_state == LevelDatas.State.levelFailed)
                _iLevelMenu.OpenDiedUI();
        }

        private void CheckLevelWin()
        {
            if (!WinCondition())
                return;
            _levelDatas.SetState(LevelDatas.State.levelWin);
        }

        private bool WinCondition()
        {
            if (_wordGenerator.currentCount < _levelDatas.wordGeneratorSetting.generateAmount)
                return false;
            if (_levelDatas.wordGeneratorDatas.GetWordHashSetCount() != 0)
                return false;
            if (_levelDatas.playerDatas.GetHp() <= 0)
                return false;

            return true;
        }

        private async void LevelWin(LevelDatas.State _state)
        {
            if (_state != LevelDatas.State.levelWin)
                return;

            await UniTask.Delay(2000);
            _iLevelMenu.OpenWinUI();
        }

        public async UniTask Start()
        {
            _iLevelMenu.CloseLevelEndUI();

            _cancellationTokenSource = new();
            await _levelStartUI.Start(_cancellationTokenSource.Token);
            
            await WordGeneratorRepeatGenerate();
        }

        private async UniTask WordGeneratorRepeatGenerate()
        {
            WordGeneratorSetting _wordGeneratorSetting = _levelDatas.wordGeneratorSetting;
            int _generateAmount = _wordGeneratorSetting.generateAmount;
            int _generateIntervalMs = _wordGeneratorSetting.generateIntervalMs;
            
            await _wordGenerator.RepeatGenerate(_generateAmount, _generateIntervalMs, _cancellationTokenSource.Token);
        }

        private void Restart()
        {
            _cancellationTokenSource.Cancel();
            _levelDatas.ReSet();
            _iLevelMenu.ReSet();
            _wordAttacker.Reset();

            AddListener();

            Start().Forget();
        }

        public void Tick()
        {
            _levelDatas.wordGeneratorDatas.TickWordHashSet();
            _wordAttacker.Tick();
            _levelDatas.wordGeneratorDatas.RemoveWordByWordWillRemoveList();
        }
    }
}