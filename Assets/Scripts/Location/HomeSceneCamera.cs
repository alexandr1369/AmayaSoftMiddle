using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace Location
{
    public class HomeSceneCamera : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        
        [Inject]
        private void Construct(HomeSceneLoadingContext context) => context.HomeSceneCamera = this;
    }
}