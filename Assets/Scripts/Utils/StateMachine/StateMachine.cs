using UnityEngine;

namespace Utils.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected abstract State InitialState { get; }
        protected virtual bool EnterOnEnable { get; set; }
        
        private State _currentState;

        private void OnEnable()
        {
            if(!EnterOnEnable)
                return;
            
            _currentState = InitialState;
            _currentState?.Enter();
        }

        private void Start()
        {
            if(EnterOnEnable)
                return;
            
            _currentState = InitialState;
            _currentState?.Enter();
        }

        private void FixedUpdate()
        {
            _currentState?.UpdatePhysics();
        }

        private void Update()
        {
            _currentState?.HandleInput();
            _currentState?.UpdateLogic();
        }

        public void ChangeState(State state)
        {
            _currentState.Exit();
            
            _currentState = state;
            _currentState.Enter();
        }
    }
}