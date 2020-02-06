using BattleCoder.Input;
using UnityEngine;

namespace BattleCoder.Test.Input
{
    public class InputTest : MonoBehaviour
    {
        public IUserInput userInput;

        // Start is called before the first frame update
        void Start()
        {
            userInput = new KeyController();
            userInput.MeleeAttackEvent += (sender, args) => print("MeleeAttack");
            userInput.ShootingAttackEvent += (sender, args) => print("ShootingAttack");
        }

        // Update is called once per frame
        void Update()
        {
            userInput.Update();
        }
    }
}