using UnityEngine;

namespace BattleCoder.Test.Animation
{
    public class TanksAnimationTest : MonoBehaviour
    {
        private int type = 0;

        private int tick = 0;

        private bool toggle = false;

        private Animator _animator;
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int IsMove = Animator.StringToHash("IsMove");

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (type)
            {
                case 0:
                    _animator.SetInteger(X, 0);
                    _animator.SetInteger(Y, -1);
                    break;

                case 1:
                    _animator.SetInteger(X, 1);
                    _animator.SetInteger(Y, 0);
                    break;

                case 2:
                    _animator.SetInteger(X, 0);
                    _animator.SetInteger(Y, 1);
                    break;

                case 3:
                    _animator.SetInteger(X, -1);
                    _animator.SetInteger(Y, 0);
                    break;
            }

            if (tick % 100 == 0)
            {
                _animator.SetBool(IsMove, toggle);
                toggle = !toggle;
            }

            if (tick++ % 200 == 0)
                type = type == 3 ? 0 : ++type;
        }
    }
}