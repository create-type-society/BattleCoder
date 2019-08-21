using UnityEngine;

public class BotEntityAnimation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int IsMove = Animator.StringToHash("IsMove");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(Direction direction, bool isMove)
    {
        int x = 0;
        int y = 0;
        switch (direction)
        {
            case Direction.Up:
                x = 0;
                y = 1;
                break;
                ;
            case Direction.Down:
                x = 0;
                y = -1;
                break;

            case Direction.Left:
                x = -1;
                y = 0;
                break;

            case Direction.Right:
                x = 1;
                y = 0;
                break;
        }

        _animator.SetInteger(X, x);
        _animator.SetInteger(Y, y);
        _animator.SetBool(IsMove, isMove);
    }

    public void ResetAnimation()
    {
        _animator.SetInteger(X, 0);
        _animator.SetInteger(Y, 0);
        _animator.SetBool(IsMove, false);
    }
}