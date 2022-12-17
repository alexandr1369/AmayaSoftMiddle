using System;
using UnityEngine;

namespace StateSystem.UserState
{
    [Serializable]
    public class UserState
    {
        [field: SerializeField] public GameSettings GameSettings { get; set; }
        [field: SerializeField] public GameData GameData { get; set; }
        [field: SerializeField] public LocationState CurrentLocation { get; private set; }
        [field: SerializeField] public LocationState PreviousLocation { get; private set; }
        
        public UserState()
        {
            GameSettings = new GameSettings();
            GameData = new GameData();
            CurrentLocation = new LocationState(LocationState.LocationSceneType.Home);
        }
        
        public bool SetLocation(LocationState locationState)
        {
            if (locationState == null || CurrentLocation.Equals(locationState))
                return false;

            PreviousLocation = CurrentLocation;
            CurrentLocation = locationState;

            return true;
        }
        
        public void SetMusic(bool state) => GameSettings.IsMusicEnabled = state;

        public void SetSound(bool state) => GameSettings.IsSoundEnabled = state;

        public void SetVibration(bool state) => GameSettings.IsVibrationEnabled = state;
    }
}