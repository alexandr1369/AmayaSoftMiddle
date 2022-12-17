using System;
using UnityEngine;

namespace StateSystem.UserState
{
    [Serializable]
    public class GameData
    {
        [field: SerializeField] public LevelData LevelData { get; private set; }
        [field: SerializeField] public int LocationSkinId { get; set; }
        [field: SerializeField] public long ApplicationQuitFileTimeUtc { get; set; }

        public GameData()
        {
            LevelData = new LevelData();
            LocationSkinId = 0;
            ApplicationQuitFileTimeUtc = DateTime.Now.ToFileTimeUtc();
        }
    }
}