using ADOFAI;
using HarmonyLib;


namespace EnhancedEffectRemover {

    public class RemoveEffects
    {
        [HarmonyPatch(typeof(LevelData), "Decode")]
        class LevelDecodePatch
        {
            static void Postfix(LevelData __instance, ref LoadResult status)
            {
                Remover.Remove(__instance);

                status = LoadResult.Successful;
            }
        }


        [HarmonyPatch(typeof(scnEditor), "SaveLevel")]
        class BlockLevelSave
        {
            static bool Prefix()
            {
                return false;
            }
        }
    }
}