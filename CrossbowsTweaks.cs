using HarmonyLib;

namespace BetterCrossbows;

public static class CrossbowsTweaks
{
    private static bool IsCrossbow(ItemDrop.ItemData itemData)
    {
        if (itemData == null) return false;

        return itemData.m_shared.m_skillType == Skills.SkillType.Crossbows;
    }

    [HarmonyPatch(typeof(ItemDrop.ItemData), "GetWeaponLoadingTime")]
    public static class GetWeaponLoadingTimePostFix
    {
        private static void Postfix(ItemDrop.ItemData __instance, ref float __result)
        {
            if (!Plugin.crossbowEnabled.Value) return;
            
            if (!IsCrossbow(__instance)) return;

            __result = Plugin.crossbowLoadingTime.Value;
        }
    }

    [HarmonyPatch(typeof(Player), "ClearActionQueue")]
    public static class ClearActionQueueFix
    {
        private static Player.MinorActionData reloadAction = null;
  
        private static bool Prefix(Player __instance)
        {
            if (!Plugin.crossbowEnabled.Value || !Plugin.reloadWhileMoving.Value) return true;
            
            reloadAction = null;

            foreach (var action in __instance.m_actionQueue)
            {
                if (action.m_type == Player.MinorActionData.ActionType.Reload)
                {
                    reloadAction = action;
                    break;
                }
            }

            return true;
        }
        
        private static void Postfix(Player __instance)
        {
            if (!Plugin.crossbowEnabled.Value || !Plugin.reloadWhileMoving.Value) return;

            if (!IsCrossbow(__instance.m_rightItem) && !IsCrossbow(__instance.m_leftItem)) return;

            if (__instance.IsReloadActionQueued()) return;

            if (reloadAction == null) return;

            __instance.m_actionQueue.Add(reloadAction);
        }
    }

    [HarmonyPatch(typeof(Player), "QueueReloadAction")]
    public static class QueueReloadActionPatch
    {
        private static void Postfix(Player __instance)
        {
            if (!Plugin.crossbowEnabled.Value || Plugin.enableReloadStaminaDrain.Value) return;
            
            var reloadAction = __instance.m_actionQueue.Find(action => 
                action.m_type == Player.MinorActionData.ActionType.Reload);
                
            if (reloadAction != null && IsCrossbow(reloadAction.m_item))
            {
                reloadAction.m_staminaDrain = 0f;
            }
        }
    }
}