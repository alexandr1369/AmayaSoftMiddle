using Location.Character.Will;
using UnityEngine;
using Zenject;

namespace Location.Character
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] private Transform WillSpawnPoint { get; set; }
        
        private WillsService _service;
        private Will.Will _will;
        
        [Inject]
        private void Construct(WillsService service) => _service = service;

        private void Start()
        {
            _will = _service.GetWill();
            _will.transform.parent = transform;
            _will.transform.position = WillSpawnPoint.position;
        }
    }
}