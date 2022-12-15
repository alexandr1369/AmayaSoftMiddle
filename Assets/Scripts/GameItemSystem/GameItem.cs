using UnityEngine;

namespace GameItemSystem
{
    [CreateAssetMenu(menuName = "Zorg/Object/New Game Item", fileName = "GameItem", order = 1)]
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
            
            return Equals((GameObject)obj);    
        }
        
        private bool Equals(Object other) => name.Equals(other.name);
    }
}