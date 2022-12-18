using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    public class InitializationInstaller : MonoInstaller
    {
        public override void InstallBindings() => InitGameData();

        private static void InitGameData() => Application.targetFrameRate = GameUtils.TARGET_FPS;
    }
}