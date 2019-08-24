using System;

public class MoveShotRotationCommandObject : ICommandObject
{
    public bool IsFinished { get; private set; }

    readonly Action changeRotationCallback;

    public MoveShotRotationCommandObject(Action changeRotationCallback)
    {
        this.changeRotationCallback = changeRotationCallback;
    }

    public void Run()
    {
        changeRotationCallback();
        IsFinished = true;
    }
}