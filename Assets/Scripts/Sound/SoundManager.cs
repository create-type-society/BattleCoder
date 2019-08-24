using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioSource firingSound;
    [SerializeField]AudioSource meleeSound;
    
    public void MakeFiringSound()
    {
        firingSound.Play();
    }
    public void MakeMeleeSound()
    {
        meleeSound.Play();
    }
    
    
}
