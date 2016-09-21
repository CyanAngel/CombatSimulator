using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class CombatSimulation
    {
        public static List<Result> SimulateFights(int numoffights,int startRange, ICombatant combatant1, ICombatant combatant2) 
        {
            List<Result> results = new List<Result>();
            for (int x = 0; x < numoffights; x++)
            {
                results.Add(SimulateFight(startRange, (ICombatant)Activator.CreateInstance(combatant1.GetType()), (ICombatant)Activator.CreateInstance(combatant2.GetType())));
            }
            return results;
        }
        public static Result SimulateFight(int startRange, ICombatant combatant1, ICombatant combatant2)
        {
            Range range = new Range { CurrentDistance = startRange };
            int roundsTaken = 0;
            while (combatant1.CurrentHP > 0 && combatant2.CurrentHP > 0 && range.CurrentDistance < (startRange * 2) && roundsTaken < 100)
            {
                roundsTaken++;
                combatant1.TakeTurn(combatant2, range);
                if (combatant2.CurrentHP>0)
                {
                    combatant2.TakeTurn(combatant1, range);
                }
            }
            ICombatant winner;
            ICombatant loser;
            if (combatant1.CurrentHP > combatant2.CurrentHP)
            {
                winner = combatant1;
                loser = combatant2;
            }
            else
            {
                winner = combatant2;
                loser = combatant1;
            }
            return new Result(winner,loser,roundsTaken,range.CurrentDistance);
        }
    }
}
