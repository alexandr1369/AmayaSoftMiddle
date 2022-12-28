using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class InputDelegate : IInputDelegate
    {
        private readonly List<IInputDelegate.InputRestriction> _restrictions = new();
        private readonly List<IInputDelegateListener> _listeners = new();

        public InputDelegate()
        {
            // Дефолтный рестрикшин - вернет всегда true.
            _restrictions.Add(_ => _restrictions.Count == 1);
        }

        /// <summary>
        /// Проверка на дозволенность инпута для таргетируемого объекта.
        /// </summary>
        /// <param name="obj">Таргер-объект.</param>
        /// <returns>Если хотя бы один делегат за интеракт с объектом - вернуть true.</returns>
        public bool HasPermission(object obj) => 
            _restrictions.Any(restriction => restriction.Invoke(obj));

        public void AddRestriction(IInputDelegate.InputRestriction inputRestriction)
        {
            if (_restrictions.Contains(inputRestriction))
                return;

            _restrictions.Add(inputRestriction);
            OnInteractionRestrictionsChanged();
        }

        public void RemoveRestriction(IInputDelegate.InputRestriction inputRestriction)
        {
            _restrictions.Remove(inputRestriction);
            OnInteractionRestrictionsChanged();
        }

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

    public interface IInputDelegate
    {
        delegate bool InputRestriction(object target);
        void AddRestriction(InputRestriction inputRestriction);
        public void RemoveRestriction(InputRestriction inputRestriction);
        /// <summary>
        /// Проверка на дозволенность инпута для таргетируемого объекта.
        /// </summary>
        /// <param name="obj">Таргер-объект.</param>
        /// <returns>Если хотя бы один делегат за интеракт с объектом - вернуть true.</returns>
        public bool HasPermission(object obj); 
    }
}