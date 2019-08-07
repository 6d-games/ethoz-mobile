#if REWIRED

using Devdog.General.Integration.RewiredIntegration;
using Devdog.General.UI;
using Devdog.InventoryPro.UI;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.RewiredIntegration
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/Rewired/Rewired UI Element Key Actions")]
    public partial class RewiredUIElementKeyActions : MonoBehaviour
    {
        [System.Serializable]
        public class KeyAction : UnityEngine.Events.UnityEvent
        {

        }

        [SerializeField]
        private RewiredHelper _helper = new RewiredHelper();


        public KeyAction keyActions = new KeyAction();

        /// <summary>
        /// The time the action has to be active before invoking the action.
        /// </summary>
        [Header("Timers")]
        public float activationTime = 0.0f;
        public bool continous = false;

        [Header("Visuals")]
        public UIShowValue visualizer = new UIShowValue();



        /// <summary>
        /// The time (duration) the action has been activated.
        /// </summary>
        private bool _firedInActiveTime;
        private UIWindow _window;

        protected virtual void Awake()
        {
            _helper.Init();
            _window = GetComponent<UIWindow>();
        }

        protected virtual void Update()
        {
            if (gameObject.activeInHierarchy == false)
                return;

            if (_window != null && _window.isVisible == false)
                return;


            var rewiredButton = _helper.player.GetButton(_helper.rewiredActionName);
            var rewiredButtonDown = _helper.player.GetButtonDown(_helper.rewiredActionName);
            if (activationTime <= 0.01f)
            {
                if (continous)
                {
                    if (rewiredButton)
                    {
                        Activate();
                    }
                }
                else
                {
                    if (rewiredButtonDown)
                    {
                        Activate();
                    }
                }

                return;
            }

            // Got a timer.

            float rewiredTimeDown = _helper.player.GetButtonTimePressed(_helper.rewiredActionName);
            visualizer.Repaint(rewiredTimeDown, 1);


            // Timer
            if (rewiredButton == false)
            {
                _firedInActiveTime = false;
            }

            // Timer check
            if (rewiredTimeDown < activationTime)
                return;

            // Time set, it's activated...
            if (continous)
            {
                if (rewiredButton)
                {
                    keyActions.Invoke();
                }
            }
            else
            {
                // Already fired / invoked events?
                if (_firedInActiveTime)
                    return;

                if (rewiredButton)
                {
                    keyActions.Invoke();
                    _firedInActiveTime = true;
                }
            }
        }

        protected virtual void Activate()
        {
            if (Devdog.General.InputManager.CanReceiveUIInput(gameObject) == false)
                return;

            keyActions.Invoke();
            visualizer.Activate();
        }
    }
}

#endif