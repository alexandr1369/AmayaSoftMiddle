using System.Collections.Generic;
using GameItemSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "AmayaSoft/Object/New Injectable Game Items Installer", fileName = "InjectableGameItemsInstaller")]
    public class InjectableGameItemsInstaller : ScriptableObjectInstaller
    {
        [field: SerializeField] private List<GameItem> Items { get; set; }
        
        public override void InstallBindings()
        {
            foreach (var item in Items) 
                Container.QueueForInject(item);
        }
    }
}