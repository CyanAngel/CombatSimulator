using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    public interface ICombatant
    {
        int AC { get; }
        int CurrentHP { get; set; }
        int StartHP { get; }
        int Movement { get; }
        int AttackRange { get; }
        string Name { get; set; }
        void TakeTurn(ICombatant enemy, Range range);
        bool DoesAttackHit(Dice roll,int modifier);
        bool MakeSave(int saveDC);
        
    }
}
