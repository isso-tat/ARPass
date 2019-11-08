using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Start
{
    public class StartInstaller : MonoInstaller
    {
        [SerializeField]
        bool _isMock;

        public override void InstallBindings()
        {
            var client = InstantiateClient();
            
            Container
                .Bind<IStartClient>()
                .FromInstance(client)
                .AsSingle();
        }

        IStartClient InstantiateClient()
        {
            if (_isMock)
            {
                return new MockStartClient();
            }

            return new StartClient();
        }
    }
}