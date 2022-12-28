using Location.ConveyorTape.Item;

namespace Location.Character
{
    public interface ICharactersInteractService
    {
        bool IsInteracting(IConveyorTapeItem item, out Character character);
        void Add(Character character);
        void Remove(Character character);
    }
}