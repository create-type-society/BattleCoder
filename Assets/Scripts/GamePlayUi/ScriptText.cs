using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ScriptText : MonoBehaviour
    {
        [SerializeField] Text scriptEditor;
        [SerializeField] Dropdown fontSize;

        public string GetScriptText()
        {
            return scriptEditor.text;
        }
        
        
        public void SetScriptFontSize()
        {
            scriptEditor.fontSize = int.Parse(fontSize.options[fontSize.value].text);
        }
    }
}