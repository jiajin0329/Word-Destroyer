using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordGeneratorDatas
    {
        // Data
        // Word Datas
        [SerializeField]
        public WordObjectPool wordObjectPool = new(10);
        private HashSet<Word> _wordHashSet = new();
        private Dictionary<string, Queue<Word>> _wordDictionar = new();
        private List<Word> _waitRemoveWordList = new();

        /// <summary>
        /// Add to WordGeneratorData's wordHashSet.
        /// </summary>
        public void AddWord(Word _word)
        {
            _wordHashSet.Add(_word);

            AddWordToDictionary(_word);
        }

        private void AddWordToDictionary(Word _word)
        {
            string _wordName = _word.GetViewTextName();
            if (_wordDictionar.ContainsKey(_wordName))
            {
                _wordDictionar[_wordName].Enqueue(_word);
            }
            else
            {
                Queue<Word> _wordQueue = new();
                _wordQueue.Enqueue(_word);
                _wordDictionar.Add(_wordName, _wordQueue);
            }
        }

        public bool WordHashSetContains(Word _word) => _wordHashSet.Contains(_word);

        public int GetWordHashSetCount() => _wordHashSet.Count;

        public int GetWordDictionarWordQueueCount(string _wordName)
        {
            if (_wordDictionar.ContainsKey(_wordName))
            {
                return _wordDictionar[_wordName].Count;
            }

            return 0;
        }

        public int GetWaitRemoveWordListCount() => _waitRemoveWordList.Count;

        /// <summary>
        /// Remove to WordGeneratorData's wordHashSet.
        /// </summary>
        public Word RemoveWord(string _wordName)
        {
            if (!_wordDictionar.ContainsKey(_wordName) || _wordDictionar[_wordName].Count < 1)
                return null;

            Word _word = _wordDictionar[_wordName].Dequeue();
            _wordHashSet.Remove(_word);
            return _word;
        }

        public void AddWaitRemoveWordList(Word _word)
        {
            _waitRemoveWordList.Add(_word);
        }

        public void RemoveWordByWordWillRemoveList()
        {
            if (_waitRemoveWordList.Count < 1)
                return;

            int i;
            Word _word;
            for (i = 0; i < _waitRemoveWordList.Count; i++)
            {
                _word = _waitRemoveWordList[i];
                _wordDictionar[_word.GetViewTextName()].Dequeue();
                _wordHashSet.Remove(_word);
                wordObjectPool.Release(_word);
            }

            _waitRemoveWordList.Clear();
        }
        
        public void TickWordHashSet()
        {
            foreach (Word _word in _wordHashSet)
            {
                _word.Tick();
            }
        }
    }
}
