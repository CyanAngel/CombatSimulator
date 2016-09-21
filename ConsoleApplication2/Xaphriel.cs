using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Xaphriel : ICombatant
    {
        public int AC
        {
            get
            {
                return 22;
            }
        }

        public int AttackRange
        {
            get
            {
                return 10;
            }
        }

        public int CurrentHP
        {
            get;

            set;
        }

        public int Movement
        {
            get;
            internal set;
        }
        private string _name = "Xaphriel";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int StartHP
        {
            get
            {
                return 107;
            }
        }
        public Xaphriel()
        {
            CurrentHP = StartHP;
            Movement = 30;
            Name = "Xaphriel Unmounted";
        }
        public bool DoesAttackHit(Dice roll, int modifier)
        {
            return (roll.Result + modifier >= AC);
        }

        public bool MakeSave(int saveDC)
        {
            return true;
        }

        public void TakeTurn(ICombatant enemy, Range range)
        {
            if (range.CurrentDistance > AttackRange)
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            if (range.CurrentDistance > AttackRange)
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            else
            {
                Dice roll = new Dice(20);
                if (enemy.DoesAttackHit(roll,12))
                {
                    enemy.CurrentHP -= DamageRoll() + 8;
                    if (roll.Result == 20) enemy.CurrentHP -= DamageRoll();
                }
            }
        }
        private void MoveRelativeEnemy(int TargetDistance, Range range)
        {
            range.CurrentDistance -= Math.Min(range.CurrentDistance - TargetDistance, Movement);
        }
        protected int DamageRoll()
        {
            return new Dice(12).Result + new Dice(8).Result;
        }
    }
    class XaphrielHorse : Xaphriel
    {
        public XaphrielHorse() : base()
        {
            Movement = 60;
            Name = "Xaphriel Mounted";
        }
    }
}
