using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col) => 
            col.GetComponent<IConveyorTapeItem>()?.Collect();
    }
}