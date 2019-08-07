#if PLY_GAME

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    public class CraftingEventHandler : plyEventHandler
    {
        public List<plyEvent> onCraftingSuccess = new List<plyEvent>();
        public List<plyEvent> onCraftingFailed = new List<plyEvent>();
        public List<plyEvent> onCraftingProgress = new List<plyEvent>();
        public List<plyEvent> onCraftingCanceled = new List<plyEvent>();


        public override void StateChanged()
        {
            onCraftingSuccess.Clear();
            onCraftingFailed.Clear();
            onCraftingProgress.Clear();
            onCraftingCanceled.Clear();
        }

        public override void AddEvent(plyEvent e)
        {
            if (e.uniqueIdent.Equals("Crafting OnCraftingSuccess"))
                onCraftingSuccess.Add(e);

            if (e.uniqueIdent.Equals("Crafting OnCraftingFailed"))
                onCraftingFailed.Add(e);

            if (e.uniqueIdent.Equals("Crafting OnCraftingProgress"))
                onCraftingProgress.Add(e);

            if (e.uniqueIdent.Equals("Crafting OnCraftingCanceled"))
                onCraftingCanceled.Add(e);
        }

        public override void CheckEvents()
        {
            enabled = onCraftingSuccess.Count > 0
                      || onCraftingFailed.Count > 0
                      || onCraftingProgress.Count > 0
                      || onCraftingCanceled.Count > 0;
        }



        public void OnCraftSuccess(CraftingProgressContainer.CraftInfo craftInfo)
        {
            if (onCraftingSuccess.Count > 0)
            {
                RunEvents(onCraftingSuccess,
                    new plyEventArg("category", craftInfo.category),
                    new plyEventArg("categoryID", (int)craftInfo.category.ID),
                    new plyEventArg("blueprint", craftInfo.blueprint),
                    new plyEventArg("blueprintID", (int)craftInfo.blueprint.ID));
            }
        }

        public void OnCraftFailed(CraftingProgressContainer.CraftInfo craftInfo)
        {
            if (onCraftingFailed.Count > 0)
            {
                RunEvents(onCraftingFailed,
                    new plyEventArg("itemID", (int)craftInfo.blueprint.resultItems.First().item.ID),
                    new plyEventArg("category", craftInfo.category),
                    new plyEventArg("categoryID", (int)craftInfo.category.ID),
                    new plyEventArg("blueprint", craftInfo.blueprint),
                    new plyEventArg("blueprintID", (int)craftInfo.blueprint.ID));
            }
        }

        public void OnCraftProgress(CraftingProgressContainer.CraftInfo craftInfo, float progress)
        {
            if (onCraftingProgress.Count > 0)
            {
                RunEvents(onCraftingProgress,
                    new plyEventArg("itemID", (int)craftInfo.blueprint.resultItems.First().item.ID),
                    new plyEventArg("category", craftInfo.category),
                    new plyEventArg("categoryID", (int)craftInfo.category.ID),
                    new plyEventArg("blueprint", craftInfo.blueprint),
                    new plyEventArg("blueprintID", (int)craftInfo.blueprint.ID),
                    new plyEventArg("progress", progress));
            }
        }

        public void OnCraftCanceled(CraftingProgressContainer.CraftInfo craftInfo, float progress)
        {
            if (onCraftingCanceled.Count > 0)
            {
                RunEvents(onCraftingCanceled,
                    new plyEventArg("itemID", (int)craftInfo.blueprint.resultItems.First().item.ID),
                    new plyEventArg("category", craftInfo.category),
                    new plyEventArg("categoryID", (int)craftInfo.category.ID),
                    new plyEventArg("blueprint", craftInfo.blueprint),
                    new plyEventArg("blueprintID", (int)craftInfo.blueprint.ID),
                    new plyEventArg("progress", progress));
            }
        }
    }
}

#endif