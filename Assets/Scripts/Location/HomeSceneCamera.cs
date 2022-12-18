using LoadingSystem.Loading.Operations.Home;
using UnityEngine;
using Zenject;

namespace Location
{
    public class HomeSceneCamera : MonoBehaviour
    {
        [field: SerializeField] private Camera Camera { get; set; }
        
        [Inject]
        private void Construct(HomeSceneLoadingContext context) => context.HomeSceneCamera = Camera;
    }
}