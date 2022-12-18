using LoadingSystem.Loading.Operations.Home;
using UnityEngine.SceneManagement;

namespace LoadingSystem.Loading.Operations
{
    public class HomeSceneLoadingSequence : LoadingSequence
    {
        public HomeSceneLoadingSequence(HomeSceneLoadingContext context)
        {
            // TODO: disable restrictions
            
            Add(new LoadSceneOperation("HomeScene", LoadSceneMode.Single));
            Add(new LoadSceneOperation("HudScene", LoadSceneMode.Additive));
            Add(new InitHomeMusicOperation(/*context.AudioService*/));
            
            // TODO: bulding, others operations and restrictions
        }
    }
}