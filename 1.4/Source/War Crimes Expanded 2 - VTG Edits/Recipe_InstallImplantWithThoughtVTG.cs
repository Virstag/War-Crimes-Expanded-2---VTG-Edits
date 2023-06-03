using System.Collections.Generic;
using RimWorld;
using Verse;
using WarCrimesExpanded2;

namespace WarCrimesExpanded2VTG

{
    public class Recipe_InstallImplantWithThoughtVTG : Recipe_InstallImplant
    {
        public override void ApplyOnPawn(Pawn victim, BodyPartRecord part, Pawn torturer, List<Thing> ingredients, Bill bill)
        {
            if (torturer != null)
            {
                if (CheckSurgeryFail(torturer, victim, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, torturer, victim);
            }
            if (IsViolationOnPawn(victim, part, Faction.OfPlayer))
            {
                ReportViolation(victim, torturer, victim.HomeFaction, -25);
            }
            WCE2_ThoughtHelper.WCE2_GiveThoughtsForPawnTortured(victim, torturer);
            victim.health.AddHediff(recipe.addsHediff, part);
            if (victim.Dead)
            {
                ThoughtUtility.GiveThoughtsForPawnExecuted(victim, torturer, PawnExecutionKind.GenericBrutal);
            }
        }
    }
}