using Location.ConveyorTape;
using Location.ConveyorTape.Item;
using UnityEngine;

namespace Editor.Tests
{
    public static class Setup
    {
        public static ConveyorTapeItem TapeItem()
        {
            var conveyorTapeItem = new GameObject().AddComponent<ConveyorTapeItem>();
            var config = Create.ConveyorTapeItemConfig();
            conveyorTapeItem.Config = config;
            conveyorTapeItem.SpriteRenderer = conveyorTapeItem.gameObject.AddComponent<SpriteRenderer>();

            return conveyorTapeItem;
        }

        public static ConveyorTape Tape() => new GameObject().AddComponent<ConveyorTape>();
        
        public static ConveyorTapeItemDraggable ItemDraggable() => 
            new GameObject().AddComponent<ConveyorTapeItemDraggable>();
    }
}