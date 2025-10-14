using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager.Initialize();
        }

        private void Start()
        {
            CancellationToken _cancellation = this.GetCancellationTokenOnDestroy();
            _levelManager.Start(_cancellation).Forget();
        }

        private void Update()
        {
            _levelManager.Tick();
        }
    }
}