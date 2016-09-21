using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Thia : ICombatant
    {
        public int AC
        {
            get
            {
                return 17;
            }
        }

        public int AttackRange
        {
            get
            {
                return 120;
            }
        }

        public int CurrentHP
        {
            get;
            set;
        }

        public int Movement
        {
            get
            {
                return 30;
            }
        }
        private string _name = "Thia";
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
                return 89;
            }
        }
        public Thia()
        {
            CurrentHP = StartHP;
        }
        public int sheildsLeft = 4;
        internal bool sheildedThisTurn = false;
        public bool DoesAttackHit(Dice roll, int modifier)
        {
            if (sheildedThisTurn)
            {
                return (roll.Result + modifier >= AC + 5);
            }
            else
            {
                if (roll.Result + modifier >= AC && roll.Result + modifier < AC + 5 && sheildsLeft > 0)
                {
                    sheildsLeft--;
                    sheildedThisTurn = true;
                    return (roll.Result + modifier >= AC + 5);
                }
                else return (roll.Result + modifier >= AC);
            }
        }

        public bool MakeSave(int saveDC)
        {
            return true;
        }
        public int mistySteps = 3;
        public int SP = 12;
        public int fireballs = 3;
        public bool castFireBallThisTurn = false;
        public void TakeTurn(ICombatant enemy, Range range)
        {
            castFireBallThisTurn = false;
            if (range.CurrentDistance > AttackRange)//Movement
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            if (range.CurrentDistance > AttackRange && mistySteps > 0)//Bonus Action - Misty Step?
            {
                mistySteps--;
                MoveRelativeEnemy(AttackRange, range);
            }
            else if (fireballs > 0 && SP > 1)//Bonus Action - Quicken Fireball?
            {
                SP -= 2;
                CastFireball(enemy);
            }
            else if (SP > 1)//Bonus Action - Quicken Firebolt?
            {
                SP -= 2;
                CastBolt(enemy);
            }
            if (range.CurrentDistance > AttackRange)//Action - Dash?
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            else if (fireballs > 0 && !castFireBallThisTurn)//Action - Fireball?
            {
                CastFireball(enemy);
            }
            else//Action - Firebolt
            {
                CastBolt(enemy);
            }
            sheildedThisTurn = false;
        }
        internal void CastBolt(ICombatant enemy)
        {
            Dice roll = new Dice(20);
            if(enemy.DoesAttackHit(roll,9))
            {
                enemy.CurrentHP -= FireboltDamageRoll();
                if (roll.Result == 20) enemy.CurrentHP -= FireboltDamageRoll();
                enemy.CurrentHP -= 5;
            }
        }
        private int FireboltDamageRoll()
        {
            return Math.Max(2, new Dice(10).Result) + Math.Max(2, new Dice(10).Result) + Math.Max(2, new Dice(10).Result);
        }
        internal void CastFireball(ICombatant enemy)
        {
            fireballs--;
            castFireBallThisTurn = true;
            int Damage = FireBallDamage() + 5;
            if (enemy.MakeSave(17))
            {
                enemy.CurrentHP -= Damage / 2;
            }
            else
            {
                enemy.CurrentHP -= Damage;
            }
        }
        private int FireBallDamage()
        {
            return Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result) +
                Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result) + Math.Max(2, new Dice(6).Result);
        }

        private void MoveRelativeEnemy(int TargetDistance, Range range)
        {
            range.CurrentDistance -= Math.Min(range.CurrentDistance - TargetDistance, Movement);
        }
    }
    class ThiaNoFireBalls : Thia
    {
        public ThiaNoFireBalls() : base()
        {
            fireballs = 0;
        }
    }
    class ThiaNoQuickening : Thia
    {
        public ThiaNoQuickening() : base()
        {
            SP = 0;
        }
    }
    class ThiaNoQuickeningOrFireballs : ThiaNoFireBalls
    {
        public ThiaNoQuickeningOrFireballs() : base()
        {
            SP = 0;
        }
    }
}
