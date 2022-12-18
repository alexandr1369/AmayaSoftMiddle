using Location;
using StateSystem;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public GameController GameController { get; set; } 
        public SceneLoadingService SceneLoadingService { get; set; }
        public AudioService AudioService { get; set; }
        public HomeSceneCamera HomeSceneCamera { get; set; }
    }
}