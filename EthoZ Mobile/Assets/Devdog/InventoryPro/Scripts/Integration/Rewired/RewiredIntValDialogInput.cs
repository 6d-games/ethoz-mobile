#if REWIRED

using Devdog.General.Integration.RewiredIntegration;
using Devdog.InventoryPro.Dialogs;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.RewiredIntegration
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/Rewired/Rewired IntValDialog Input")]
    [RequireComponent(typeof(IntValDialog))]
    public sealed class RewiredIntValDialogInput : MonoBehaviour
    {
        [SerializeField]
        private RewiredHelper _helper = new RewiredHelper();
        private IntValDialog _dialog;

        private void Awake()
        {
            _helper.Init();
            _dialog = GetComponent<IntValDialog>();
        }

        private void Update()
        {
            if (_helper.player.GetButtonDown(_helper.rewiredActionName))
            {
                _dialog.AddToInputValue(1);
            }
            else if (_helper.player.GetNegativeButtonDown(_helper.rewiredActionName))
            {
                _dialog.AddToInputValue(-1);
            }
        }
    }
}

#endif