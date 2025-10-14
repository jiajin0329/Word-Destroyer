using UnityEngine;

namespace Logy.WordDestroyer
{
    [CreateAssetMenu(fileName = nameof(WordGeneratorSetting), menuName = "Scriptable Objects/" + nameof(WordGeneratorSetting))]
    public class WordGeneratorSetting : ScriptableObject
    {
        [field: SerializeField]
        public int generateAmount { get; private set; } = 100;
        [field: SerializeField]
        public int generateIntervalMs { get; private set; } = 500;
        [field: SerializeField]
        public string[] wordNames { get; private set; } = new string[1] { "Test" };
        [field: SerializeField]
        public Vector3 generateMinPosition { get; private set; }
        [field: SerializeField]
        public Vector3 generateMaxPosition { get; private set; }
    }
}


