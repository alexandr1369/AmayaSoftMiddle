using Backpack;
using Location;
using Location.ConveyorTape;
using UI;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public SceneLoadingService SceneLoadingService { get; set; }
        public AudioService AudioService { get; set; }
        public Inventory Inventory { get; set; }
        public HudItems HudItems { get; set; }
        public HomeSceneCamera HomeSceneCamera { get; set; }
        public ConveyorTapeBuildingService ConveyorTapeBuildingService { get; set; }
        public ConveyorTape ConveyorTape { get; set; }
    }
}