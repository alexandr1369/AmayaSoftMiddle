using System;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class AwaitingItemState : ItemState
    {
        [field: SerializeField] public long AwaitingStartFileTimeUtc { get; set; }
        
        public AwaitingItemState(string id) : base(id)
        {
        }

        public float GetPassedSeconds()
        {
            var startDateTime = DateTime.FromFileTime(AwaitingStartFileTimeUtc);
            
            return (float)(DateTime.Now - startDateTime).TotalSeconds;
        }
        
        public bool IsDone(float delay) => GetPassedSeconds() >= delay;
    }
}