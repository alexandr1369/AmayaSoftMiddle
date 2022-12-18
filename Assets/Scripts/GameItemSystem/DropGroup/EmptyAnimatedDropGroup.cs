using System.Collections.Generic;
using UnityEngine;

namespace GameItemSystem.DropGroup
{
    /// <summary>
    /// Use it when you need drop group items animation for your drop item array
    /// without custom animated drop group creation.
    /// </summary>
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Empty Animated Drop Group", fileName = "EmptyAnimatedDropGroup")]
    public class EmptyAnimatedDropGroup : AnimatedDropGroup
    {
        public void Init(List<DropItem> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }
    }
}