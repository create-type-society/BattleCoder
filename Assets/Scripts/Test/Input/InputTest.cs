using BattleCoder.AttackInput;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleCoder.Test.Input
{
    public class InputTest : MonoBehaviour
    {
        [FormerlySerializedAs("userInput")] public IAttackInput attackInput;

        // Start is called before the first frame update
        void Start()
        {
            attackInput = new KeyAttackInput();
            attackInput.MeleeAttackEvent += (sender, args) => print("MeleeAttack");
            attackInput.ShootingAttackEvent += (sender, args) => print("ShootingAttack");
        }

        // Update is called once per frame
        void Update()
        {
            attackInput.Update();
        }
    }
}