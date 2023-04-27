using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Items.Usables;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Better268.Patches;

[HarmonyPatch(typeof(Scp268), nameof(Scp268.ServerOnUsingCompleted))]
internal static class Scp268Cooldown
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUsingCompleted(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

        const int offset = -2;
        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Call &&
                                                             instruction.operand is MethodInfo
                                                             {
                                                                 Name: nameof(Scp268.ServerSetPersonalCooldown)
                                                             }) + offset;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldc_R4, MainClass.Singleton.Config.CustomCooldown),
            new(OpCodes.Call, Method(typeof(Scp268), nameof(Scp268.ServerSetPersonalCooldown), new []{typeof(float)})),
            new(OpCodes.Ret)
        });

        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}