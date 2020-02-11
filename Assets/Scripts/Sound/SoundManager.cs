using UnityEngine;

namespace BattleCoder.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource firingSound;
        [SerializeField] AudioSource meleeSound;
        [SerializeField] AudioSource decisionSound;

        public void MakeFiringSound()
        {
            firingSound.PlayOneShot(firingSound.clip);
        }

        public void MakeMeleeSound()
        {
            meleeSound.PlayOneShot(meleeSound.clip);
        }

        public void MakeDecisionSound()
        {
            decisionSound.PlayOneShot(decisionSound.clip);
        }
    }
}