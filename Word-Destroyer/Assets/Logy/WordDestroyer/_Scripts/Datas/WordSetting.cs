using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [CreateAssetMenu(fileName = nameof(WordSetting), menuName = "Scriptable Objects/" + nameof(WordSetting))]
    public class WordSetting : ScriptableObject
    {
        [field: SerializeField]
        public GameObject wordPrefab { get; private set; }
        
        [field: SerializeField]
        public WordStat[] wordStats { get; private set; }

        public WordSetting(GameObject _wordPrefabs, WordStat[] _wordStats)
        {
            wordPrefab = _wordPrefabs;
            wordStats = _wordStats;
        }

        public static WordSetting BuildTestSetting()
        {
            GameObject _gameObject = new GameObject();
            _gameObject.AddComponent<TextMeshPro>();
            GameObject _wordPrefab = _gameObject;
            WordStat[] _wordStats = new WordStat[1] { new WordStat() };

            return new WordSetting(_wordPrefab, _wordStats);
        }
    }
}