using StateSystem;
using UnityEngine;

namespace LoadingSystem.Loading.Operations.Home
{
    public class HomeSceneLoadingContext
    {
        public GameController GameController { get; set; } 
        public SceneLoadingService SceneLoadingService { get; set; }
        public AudioService AudioService { get; set; }
        public Camera HomeSceneCamera { get; set; }
    }
}