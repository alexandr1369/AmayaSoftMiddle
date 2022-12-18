using Backpack;
using Location;
using Location.ConveyorTape;
using StateSystem;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public GameController GameController { get; set; } 
        public SceneLoadingService SceneLoadingService { get; set; }
        public AudioService AudioService { get; set; }
        public Inventory Inventory { get; set; }
        public HomeSceneCamera HomeSceneCamera { get; set; }
        public ConveyorTapeBuildingService ConveyorTapeBuildingService { get; set; }
        public ConveyorTape ConveyorTape { get; set; }
    }
}