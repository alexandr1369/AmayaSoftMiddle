using System;
using UnityEngine;

namespace StateSystem.UserState
{
    [Serializable]
    public class LevelData : ICloneable
    {
        public const int DEFAULT_INDEX = 1;
        public const int COMPLETE_STAGE_INDEX = 5;

        [field: NonSerialized] public event Action<int> OnStageChanged;
        [field: NonSerialized] public event Action<int> OnLevelUp;
        
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public int StageIndex { get; private set; }
        [field: NonSerialized] public LevelData PreviousLevelDataBackup { get; private set; }
        
        public LevelData()
        {
            Index = DEFAULT_INDEX;
            ResetStages();
        }

        public void ResetStages()
        {
            PreviousLevelDataBackup = (LevelData)Clone();
            StageIndex = DEFAULT_INDEX;
            
            OnStageChanged?.Invoke(StageIndex);
        }

        /// <summary>
        /// Complete current game stage.
        /// </summary>
        /// <returns>true if level index has been upgraded..</returns>
        public bool CompleteStage()
        {
            PreviousLevelDataBackup = (LevelData)Clone();
            var hasLevelUp = false;
            StageIndex++;
            
            if (StageIndex > COMPLETE_STAGE_INDEX)
            {
                hasLevelUp = true;
                Index++;
                StageIndex = DEFAULT_INDEX;
                
                OnLevelUp?.Invoke(Index);
            }
            
            OnStageChanged?.Invoke(StageIndex);
            
            return hasLevelUp;
        }

        public void RollBackLevelData()
        {
            if (PreviousLevelDataBackup == null)
                return;

            Index = PreviousLevelDataBackup.Index;
            StageIndex = PreviousLevelDataBackup.StageIndex;
            OnStageChanged?.Invoke(StageIndex);
        }

        public object Clone() => MemberwiseClone();
    }
}