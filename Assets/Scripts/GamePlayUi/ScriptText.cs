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
    }
}