using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using NorthwoodLib.Pools;

namespace Better268.Patches;

[HarmonyPatch(typeof(PlayerInteract))]
internal static class InvisibleInteract
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnInteract(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
        
        newInstructions.RemoveRange(0, newInstructions.Count);
        
        newInstructions.Add(new CodeInstruction(OpCodes.Ret));

        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}