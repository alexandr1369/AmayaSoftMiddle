using UnityEngine;

namespace ConveyorTape
{
    public class ConveyorTapeBuildingService : MonoBehaviour
    {
        [field: SerializeField] private ConveyorTapeBuildingServiceConfig Config { get; set; }

        private void Start()
        {
            BuildTape();
        }

        public void BuildTape()
        {
            var tapePrefab = Config.ConveyorTapePartPrefab;
            
            
        }
    }
}