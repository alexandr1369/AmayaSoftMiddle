using System.Collections.Generic;
using Location.ConveyorTape.Item;
using UnityEngine;

namespace Location.Character
{
    public class CharactersInteractService : MonoBehaviour, ICharactersInteractService
    {
        private readonly List<Character> _characters = new();

        public bool IsInteracting(IConveyorTapeItem item, out Character character)
        {
            var interactingCharacter = _characters.Find(character => 
                character.IsInteracting(((MonoBehaviour)item).transform) 
                && (character.Wish.Item == item.Item || item.IsBonus));

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

        public void Remove(Character character) => _characters.Remove(character);
    }
}