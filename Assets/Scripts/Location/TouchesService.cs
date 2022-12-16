using System.Collections.Generic;
using UnityEngine;

namespace Location
{
    public class TouchesService : MonoBehaviour
    {
        private readonly List<Touch> _reservedTouches = new();
        
        private void Awake() => Input.multiTouchEnabled = true;
        
        public bool HasFreeTouch() => GetFreeTouch().HasValue;

        public Touch? ReserveTouch()
        {
            var touch = GetFreeTouch();
            
            if(!touch.HasValue)
                return null;
            
            _reservedTouches.Add(touch.Value);
            
            return touch;
        }

        private Touch? GetFreeTouch()
        {
            var touches = new List<Touch>(Input.touches);
            
            foreach(var touch in _reservedTouches)
                touches.Remove(touch);
            
            if(touches.Count > 0)
                return touches[0];
            
            return null;
        }
    }
}