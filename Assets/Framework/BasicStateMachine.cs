
/**
 * Implements a basic state machine. handle() methods are expected to be called by the subclass.
 * **/
public abstract class BasicStateMachine<S> : UnityEngine.MonoBehaviour where S : System.Enum
{

    private S lastState, currentState;

    public BasicStateMachine(S initialState)
    {
        lastState = initialState;
        currentState = initialState;
    }

    protected abstract S HandleRequestedState(S requestedState);
    protected abstract void HandleCurrentState(S currentState);
    
    public void RequestState(S requestedState)
    {
        if(!requestedState.Equals(currentState))
        {
            currentState = HandleRequestedState(requestedState);
        }
    }

    public void Reset(S state)
    {
        lastState = state;
        currentState = state;
    }

    public void SetCurrentState(S state)
    {
        this.currentState = state;
    }

    public S GetCurrentState()
    {
        return currentState;
    }

    public S getLastState()
    {
        return lastState;
    }
}
