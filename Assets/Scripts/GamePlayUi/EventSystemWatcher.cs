using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class EventSystemWatcher : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;

        private Dictionary<int, Component> cacheComponents = new Dictionary<int, Component>();

        public bool IsInputFieldFocused()
        {
            GameObject currentSelectedGameObject;
            return eventSystem.isFocused &&
                   (currentSelectedGameObject = eventSystem.currentSelectedGameObject) != null &&
                   GetCacheComponent<InputField>(currentSelectedGameObject);
        }

        private T GetCacheComponent<T>(GameObject gameObject) where T : Component
        {
            int hash = gameObject.GetInstanceID();
            if (cacheComponents.ContainsKey(hash) && cacheComponents[hash].GetType() == typeof(T))
            {
                return (T) cacheComponents[hash];
            }

            T component = gameObject.GetComponent<T>();
            if (component == null)
                return null;

            cacheComponents[hash] = component;

            return component;
        }
    }
}