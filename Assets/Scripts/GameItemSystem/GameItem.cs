using UnityEngine;

namespace GameItemSystem
{
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Game Item", fileName = "GameItem")]
    public class GameItem : ScriptableObject
    {
        [field: Header("Base")]
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string ReadableName { get; private set; }

        public override int GetHashCode() => name.GetHashCode();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || GetType() != obj.GetType())
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            
            return Equals((Object)obj);    
        }
        
        private bool Equals(Object other) => name.Equals(other.name);
    }
}