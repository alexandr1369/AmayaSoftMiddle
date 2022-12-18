using Location.ConveyorTape.Item;
using UnityEngine;

namespace Editor.Tests
{
    public static class Create
    {
        public static ConveyorTapeItemConfig ConveyorTapeItemConfig() => 
            Resources.Load<ConveyorTapeItemConfig>("Items/ItemConfigs/ConveyorTapeItemConfig");
    }
}