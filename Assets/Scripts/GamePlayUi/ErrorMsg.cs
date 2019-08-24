using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ErrorMsg : MonoBehaviour
    {
        [SerializeField] Text text;

        //エラーtextのセット
        public void SetText(string text)
        {
            this.text.text = text;
            var color = this.text.color;
            color.a = 1;
            this.text.color = color;
        }

        void Update()
        {
            var color = text.color;
            color.a -= 0.005f;
            text.color = color;
        }
    }
}