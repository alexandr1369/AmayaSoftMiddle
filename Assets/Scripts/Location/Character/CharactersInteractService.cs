using System.Collections.Generic;
using Location.ConveyorTape.Item;
using UnityEngine;

namespace Location.Character
{
    public class CharactersInteractService : MonoBehaviour
    {
        private readonly List<Character> _characters = new();

        public bool IsInteracting(ConveyorTapeItem item, out Vector3 mouthPosition)
        {
            var interactingCharacter = _characters.Find(character => 
                character.IsInteracting(item.transform) && character.Will.Item == item.Item);

            if (!interactingCharacter)
            {
                mouthPosition = Vector3.zero;
                
                return false;
            }
            
            mouthPosition = interactingCharacter.Mouth.position;
            
            return true;
        }
        
        public void Add(Character character)
        {
            if(_characters.Contains(character))
                return;
            
            _characters.Add(character);   
        }

        public void Remove(Character character)
        {
            if(!_characters.Contains(character))
                return;
            
            _characters.Add(character); 
        }
    }
}