using System.Collections.Generic;
using RimWorld;
using Verse;
using WarCrimesExpanded2;

namespace WarCrimesExpanded2VTG

{
    public class Recipe_RemoveBodyPartVTG_Torture : Recipe_InstallImplant
    {
        public ThingDef ProducedThingDef
        {
            get
            {
                if (recipe.specialProducts != null)
                {
                    return null;
                }
                if (recipe.products == null || recipe.products.Count != 1)
                {
                    return null;
                }
                return recipe.products[0].thingDef;
            }
        }

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
            victim.TakeDamage(new DamageInfo(WCE2_DefOf.WCE2_CutTorture, 9999f, 999f, -1f, null, part));
            if (victim.Dead)
            {
                ThoughtUtility.GiveThoughtsForPawnExecuted(victim, torturer, PawnExecutionKind.GenericBrutal);
            }
        }
    }

}


