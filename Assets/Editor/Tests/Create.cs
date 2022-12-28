using GameItemSystem;
using Location.ConveyorTape;
using Location.ConveyorTape.Item;
using UnityEngine;

namespace Editor.Tests
{
    public static class Create
    {
        public static AudioSource AudioSource() => new GameObject().AddComponent<AudioSource>();

        public static AudioClip AudioClip() => Resources.Load<AudioClip>("Audio/Click");
        
        public static GameItems GameItems() => Resources.Load<GameItems>("RuntimeAssets/GameItemCollection");
        
        public static IConveyorTape Tape() => new GameObject().AddComponent<ConveyorTape>();

        public static ConveyorTapeItemDraggable ItemDraggable() => 
            new GameObject().AddComponent<ConveyorTapeItemDraggable>();
        
        public static ConveyorTapeItemConfig ConveyorTapeItemConfig() => 
            Resources.Load<ConveyorTapeItemConfig>("Items/ItemConfigs/ConveyorTapeItemConfig");
    }
}