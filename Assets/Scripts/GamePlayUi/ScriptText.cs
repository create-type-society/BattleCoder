using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ScriptText : MonoBehaviour
    {
        [SerializeField] Text scriptEditor;

        public string GetScriptText()
        {
            return scriptEditor.text;
        }

        public void SetScriptFontSize()
        {
            scriptEditor.fontSize = Int32.Parse( /*text*/"25");
        }
    }
}