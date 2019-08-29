using System;

public class MoveShotRotationCommandObject : BaseCommandObject<Void>
{
    readonly Action changeRotationCallback;

    public MoveShotRotationCommandObject(Action changeRotationCallback)
    {
        this.changeRotationCallback = changeRotationCallback;
    }

    public override void Run()
    {
        changeRotationCallback();
        Finished();
    }
}