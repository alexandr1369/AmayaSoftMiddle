using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class InventoryState
    {
        public const string NONE_ID = "None";
        
        [field: NonSerialized] public event Action<string> OnSkinChanged;
        [field: NonSerialized] public event Action<string> OnLocationChanged;
        
        [field: SerializeField] public string SkinId { get; private set; }
        [field: SerializeField] public string LocationId { get; private set; }
        [field: SerializeField] private List<string> ReceivedSkinsIds { get; set; }
        [field: SerializeField] private List<string> ReceivedLocationsIds { get; set; }
        [field: SerializeField] private List<string> CheckedSkinsIds { get; set; }
        [field: SerializeField] private List<string> CheckedLocationsIds { get; set; }
        [field: SerializeField] public bool IsInitialized { get; set; }

        public InventoryState()
        {
            SkinId = NONE_ID;
            LocationId = NONE_ID;
            ReceivedSkinsIds = new List<string>();
            ReceivedLocationsIds = new List<string>();
            CheckedSkinsIds = new List<string>();
            CheckedLocationsIds = new List<string>();
        }
        
        public bool IsSkinReceived(string id) => ReceivedSkinsIds.Contains(id);

        public bool IsLocationReceived(string id) => ReceivedLocationsIds.Contains(id);

        public bool HasAnyNewSkin() => ReceivedSkinsIds.Count > CheckedSkinsIds.Count;

        public bool HasAnyNewLocation() => ReceivedLocationsIds.Count > CheckedLocationsIds.Count;

        public bool IsSkinNew(string id) => IsSkinReceived(id) && !CheckedSkinsIds.Contains(id);

        public bool IsLocationNew(string id) => IsLocationReceived(id) && !CheckedLocationsIds.Contains(id);

        public void AddReceivedSkinId(string id)
        {
            if (!IsSkinReceived(id))
                ReceivedSkinsIds.Add(id);
            else
                Debug.LogWarning($"[Zorg] Skin index {id} has already been purchased!");
        }
        
        public void AddReceivedLocationId(string id)
        {
            if (!IsLocationReceived(id))
                ReceivedLocationsIds.Add(id);
            else
                Debug.LogWarning($"[Zorg] Location index {id} has already been purchased!");
        }

        public void RemoveReceivedSkinId(string id)
        {
            ReceivedSkinsIds.Remove(id);

            if (!SkinId.Equals(id)) 
                return;
            
            var newSkinId = ReceivedSkinsIds[UnityEngine.Random.Range(0, ReceivedSkinsIds.Count)];
            SetSkinId(newSkinId);
        }
        
        public void SetSkinId(string id)
        {
            if(!CheckedSkinsIds.Contains(id)) 
                CheckedSkinsIds.Add(id);

            SkinId = id;
            
            OnSkinChanged?.Invoke(id);
        }
        
        public void SetLocationId(string id)
        {
            if(!CheckedLocationsIds.Contains(id)) 
                CheckedLocationsIds.Add(id);

            LocationId = id;
            
            OnLocationChanged?.Invoke(LocationId);
        }
    }
}