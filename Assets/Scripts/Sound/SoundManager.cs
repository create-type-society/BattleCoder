using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioSource firingSound;
    [SerializeField]AudioSource meleeSound;
    
    public void MakeFiringSound()
    {
        firingSound.PlayOneShot(firingSound.clip);
    }
    public void MakeMeleeSound()
    {
        meleeSound.PlayOneShot(meleeSound.clip);
    }
}
