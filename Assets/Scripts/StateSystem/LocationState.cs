using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace StateSystem
{
    [Serializable]
    public class LocationState
    {
        [field: SerializeField] public LocationSceneType SceneType { get; private set; }
        [field: SerializeField] public string Id { get; private set; }

        public LocationState(LocationSceneType sceneType, string id = null)
        {
            SceneType = sceneType;
            Id = id;
        }

        public bool IsHome() => Equals(SceneType, LocationSceneType.Home);

        protected bool Equals(LocationState other)
        {
            var idsEqual = string.IsNullOrEmpty(Id) 
                ? string.IsNullOrEmpty(other.Id)
                : Equals(Id, other.Id);
            
            return SceneType == other.SceneType && idsEqual;
        }

        [Serializable]
        public enum LocationSceneType
        {
            [EnumMember] Home
        }
    }
}