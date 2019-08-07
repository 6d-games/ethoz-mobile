#if UFPS

using Devdog.General;
using Devdog.General.UI;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.UFPS
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/UFPS/Inventory UFPS Input Controller")]
    public partial class InventoryUFPSInputController : MonoBehaviour, IPlayerInputCallbacks
    {
        [Required]
        public vp_FPInput input;

        [Required]
        public vp_SimpleCrosshair crosshair;
        public bool hideCrosshairOnBlockingWindow = true;

        /// <summary>
        /// Close the windows when you click on the world.
        /// </summary>
        public bool closeWindowsWhenClickWorld;

        /// <summary>
        /// Auto hide the cursor when windows are shown / hidden
        /// </summary>
        public bool hideCursorOnNoBlockingWindows = true;

        /// <summary>
        /// When the user pressed a button (for example W) to move forward, should the windows be closed automatically?
        /// </summary>
        public bool hideWindowsOnMovementInput = true;

        private static float lastWindowShownTime = 0.0f;

        // Start, to make sure all Awakes are done.
        protected virtual void Start()
        {
            if (hideCursorOnNoBlockingWindows)
            {
                Cursor.visible = false;
            }
        }

        protected void Update()
        {
            // Auto close window when movement is pressed.
            if (vp_Input.GetAxisRaw("Horizontal") != 0.0f || vp_Input.GetAxisRaw("Vertical") != 0.0f)
            {
                if (Time.time > lastWindowShownTime + 0.4f && hideWindowsOnMovementInput)
                {
                    HideAllInputWindows();
                }
            }

            if (closeWindowsWhenClickWorld)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (UIUtility.isHoveringUIElement == false)
                    {
                        input.MouseCursorBlocksMouseLook = true;
                        HideAllInputWindows();
                    }
                }
            }
        }

        public virtual void SetInputActive(bool set)
        {
            input.enabled = set;
            vp_Utility.LockCursor = set;

            if (hideCursorOnNoBlockingWindows)
                Cursor.visible = !set;

            if (hideCrosshairOnBlockingWindow)
                crosshair.enabled = set;
        }

        protected virtual void HideAllInputWindows()
        {
            foreach (var window in UIWindowUtility.GetAllWindowsWithInput())
            {
                window.Hide();
            }
        }
    }
}

#endif