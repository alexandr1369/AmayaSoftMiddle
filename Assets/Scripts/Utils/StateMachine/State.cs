namespace Utils.StateMachine
{
    public abstract class State
    {
        public virtual void Enter() { }
        
        public virtual void UpdatePhysics() { }
        
        public virtual void UpdateLogic () { }

        public virtual void HandleInput() { }
        
        public virtual void Exit() { }
    }
}