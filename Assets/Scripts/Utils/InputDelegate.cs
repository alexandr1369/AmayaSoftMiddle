using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class InputDelegate
    {
        private readonly List<InputRestriction> _restrictions = new();
        private readonly List<IInputDelegateListener> _listeners = new();

        public delegate bool InputRestriction(object target);
    
        public InputDelegate()
        {
            // Дефолтный рестрикшин - вернет всегда true.
            _restrictions.Add(_ => _restrictions.Count == 1);
        }

        public void AddRestriction(InputRestriction inputRestriction)
        {
            if (_restrictions.Contains(inputRestriction))
                return;

            _restrictions.Add(inputRestriction);
            OnInteractionRestrictionsChanged();
        }

        public void RemoveRestriction(InputRestriction inputRestriction)
        {
            _restrictions.Remove(inputRestriction);
            OnInteractionRestrictionsChanged();
        }

        /// <summary>
        /// Проверка на дозволенность инпута для таргетируемого объекта.
        /// </summary>
        /// <param name="obj">Таргер-объект.</param>
        /// <returns>Если хотя бы один делегат за интеракт с объектом - вернуть true.</returns>
        public bool HasPermission(object obj) => 
            _restrictions.Any(restriction => restriction.Invoke(obj));

        private void OnInteractionRestrictionsChanged() => 
            _listeners.ForEach(t => t.OnInteractionsRestrictionsChanged());

        public void AddListener(IInputDelegateListener listener)
        {
            if (_listeners.Contains(listener))
                return;

            _listeners.Add(listener);
        }

        public void RemoveListener(IInputDelegateListener listener) => _listeners.Remove(listener);
    }
    
    public interface IInputDelegateListener
    {
        void OnInteractionsRestrictionsChanged();
    }
}