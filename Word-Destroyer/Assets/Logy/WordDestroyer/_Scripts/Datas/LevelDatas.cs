using System;
using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    /// <summary>
    /// Aggregate all dynamic and static data from Level.
    /// </summary>
    [Serializable]
    public class LevelDatas
    {
        // Setting
        [field: SerializeField]
        public WordSetting wordSetting { get; private set; }
        [field: SerializeField]
        public WordGeneratorSetting wordGeneratorSetting { get; private set; }

        // Data
        [field: SerializeField]
        public WordGeneratorDatas wordGeneratorDatas { get; private set; } = new();
        [field: SerializeField]
        public PlayerDatas playerDatas { get; private set; } = new();
        [SerializeField]
        private State _state;
        private UnityAction<State> _setStateEvent;

        public enum State
        {
            levelStart,
            levelFailed,
            levelWin
        }

        public LevelDatas() {}

        public LevelDatas(WordSetting _wordSetting, WordGeneratorSetting _wordGeneratorSetting)
        {
            wordSetting = _wordSetting;
            wordGeneratorSetting = _wordGeneratorSetting;
        }

        public void Initialize()
        {
            wordGeneratorDatas.wordObjectPool.Initialize(wordSetting);
            playerDatas.Initialize();
        }

        public void ReSet()
        {
            wordGeneratorDatas.ReSet();
            playerDatas.ReSet();
            _state = State.levelStart;
            _setStateEvent = null;
        }

        public State GetState() => _state;

        public void SetState(State _set)
        {
            if (_set == _state)
                return;

            _state = _set;
            _setStateEvent?.Invoke(_set);
        }
        
        public void AddSetStateListener(UnityAction<State> _listener) => _setStateEvent += _listener;

        public void RemoveSetStateListener(UnityAction<State> _listener) => _setStateEvent -= _listener;

        public static LevelDatas BuildTestDatas()
        {
            WordGeneratorSetting _wordGeneratorSetting = new();
            LevelDatas _levelDatas = new(WordSetting.BuildTestSetting(), _wordGeneratorSetting);
            _levelDatas.Initialize();
            return _levelDatas;
        }
    }
}
