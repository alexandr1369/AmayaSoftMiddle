using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace LoadingSystem.Loading.Operations
{
    public class LoadingSequence : LoadingOperation
    {
        private readonly List<LoadingOperation> _operations = new();
        private int _currentOperationIndex;
        private bool _loadingInProgress;

        public void Add(LoadingOperation operation)
        {
            if (!_loadingInProgress && !_operations.Contains(operation)) 
                _operations.Add(operation);
        }
        
        public override async UniTask Load()
        {
            _loadingInProgress = true;

            for (_currentOperationIndex = 0; _currentOperationIndex < _operations.Count; _currentOperationIndex++)
                await _operations[_currentOperationIndex].Load();
        }

        public override float Weight() => _operations.Sum(o => o.Weight());

        private float CurrentWeight()
        {
            var result = 0f;

            if (_currentOperationIndex >= _operations.Count)
                return Weight();

            for (var i = _currentOperationIndex; i >= 0; i--)
            {
                var op = _operations[i];

                if (op is LoadingSequence seq)
                    result += seq.CurrentWeight();
                else
                    result += 1;
            }

            return result;
        }

        public float Progress()
        {
            if (_currentOperationIndex >= _operations.Count)
                return 1f;

            var finishedPercent = CurrentWeight() / Weight();
            
            return finishedPercent;
        }
    }
}