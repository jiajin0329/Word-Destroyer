using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public interface WordModel
    {
        public WordStat stat  { get; }
        public Vector3 position { get; }
        public Queue<string> attackableWord { get; }

        public event UnityAction tickEvent;
        public event UnityAction attackAction;

        public void Tick();
    }
}