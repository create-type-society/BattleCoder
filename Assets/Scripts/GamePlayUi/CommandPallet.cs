using UnityEngine;
using UnityEngine.UI;

public class CommandPallet : MonoBehaviour
{
    int cnt = 0;
    [SerializeField] InputField inputField;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cnt++;
        if (cnt % 80 == 0)
        {
            var text = inputField.text;
            inputField.text = text.Insert(inputField.caretPosition, UnityEngine.Random.value + "");
        }
    }
}