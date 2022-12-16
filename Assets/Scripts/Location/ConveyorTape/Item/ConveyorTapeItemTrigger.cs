using System;
using UnityEngine;

namespace Location.ConveyorTape.Item
{
    public class ConveyorTapeItemTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var item = col.GetComponent<ConveyorTapeItem>();
            
            if(!item)
                return;
            
            // item.
        }
    }
}