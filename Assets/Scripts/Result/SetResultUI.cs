﻿using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.Result
{
    public class SetResultUI : MonoBehaviour
    {
        [SerializeField] private Text resuntText;
    
        void Start()
        { 
            CheckResult();
        }

        private void CheckResult()
        {
            string text;
            Color color;
            if (ResultInfo.GetResult())
            {
                color = Color.red;
                text = "YOU WIN";
            }
            else
            {   
            
                color = new Color(0,65,255);
                text = "YOU LOSE";
            }
            SetText(text,color);
        }

        private void SetText(string text, Color color)
        {
            resuntText.color = color;
            resuntText.text = text;
        }
    
    }
}
