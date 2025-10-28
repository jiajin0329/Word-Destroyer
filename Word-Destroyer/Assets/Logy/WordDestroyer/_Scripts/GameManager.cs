using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelManager _levelManager = new(new LevelDatas(), new(new()), null, new());

        private void Awake()
        {
            _levelManager.Initialize();
        }

        private async UniTaskVoid Start()
        {
            await _levelManager.Start();
        }

        private void Update()
        {
            _levelManager.Tick();
        }
    }
}