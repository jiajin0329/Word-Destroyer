using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public interface ILevelMenu
    {
        public void ReSet();
        public void CloseLevelEndUI();
        public void OpenDiedUI();
        public void OpenWinUI();
        public void AddRestartButtonListener(UnityAction _unityAction);

    }
}