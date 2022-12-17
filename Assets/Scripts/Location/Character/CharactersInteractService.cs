using System.Collections.Generic;
using Location.ConveyorTape.Item;
using UnityEngine;

namespace Location.Character
{
    public class CharactersInteractService : MonoBehaviour
    {
        private readonly List<Character> _characters = new();

        public bool IsInteracting(ConveyorTapeItem item, out Character character)
        {
            var interactingCharacter = _characters.Find(character => 
                character.IsInteracting(item.transform) && character.Wish.Item == item.Item);

            if (!interactingCharacter)
            {
                character = null;
                
                return false;
            }
            
            character = interactingCharacter;
            
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