using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ScriptClearButton : MonoBehaviour
    {
        [SerializeField] InputField inputField;

        public void OnClick()
        {
            inputField.text = "";
        }
    }
}