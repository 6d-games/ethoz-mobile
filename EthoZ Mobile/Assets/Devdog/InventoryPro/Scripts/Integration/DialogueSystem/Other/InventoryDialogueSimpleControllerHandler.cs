#if DIALOGUE_SYSTEM

using Devdog.General;
using Devdog.General.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.DialogueSystem
{
    [RequireComponent(typeof(SimpleController))]
    public class InventoryDialogueSimpleControllerHandler : MonoBehaviour
    {
        [Required]
        public SimpleController simpleController;


        private int _windowCounter = 0;
        private bool _registered = true;

        protected virtual void Start()
        {
            var windows = UIWindowUtility.GetAllWindowsWithInput();
            foreach (var window in windows)
            {
                if (window.isVisible)
                    _windowCounter++;

                window.OnShow += () =>
                {
                    _windowCounter++;

                    if (_windowCounter > 0 && _registered)
                        SetActive(false);
                };

                window.OnHide += () =>
                {
                    _windowCounter--;

                    if (_windowCounter == 0 && _registered == false)
                        SetActive(true);
                };
            }

            if (_windowCounter > 0)
            {
                SetActive(false);
                //Cursor.visible = true;
            }
        }

        private void SetActive(bool active)
        {
            _registered = active;
            simpleController.enabled = active;
        }
    }
}

#endif