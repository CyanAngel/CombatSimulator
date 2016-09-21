using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class FiddleSticks : ICombatant
    {
        private int _AC = 13;
        public int AC
        {
            get
            {
                return _AC;
            }
            set
            {
                _AC = value;
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
        private string _name = "FiddleSticks";
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

        public int AttackRange
        {
            get
            {
                return 60;
            }
        }

        public int StartHP
        {
            get
            {
                return 85;
            }
        }
        internal int luckpoints = 3;
        internal int cuttingwords = 5;
        public FiddleSticks()
        {
            CurrentHP = StartHP;
        }
        public bool DoesAttackHit(Dice roll, int modifer)
        {
            if (enemyHasDisadvantage)
            {
                enemyHasDisadvantage = false;
                roll.Result = new List<Dice> { new Dice(20), roll}.OrderBy(x => x.Result).First().Result;
            }
            if (cuttingwords > 0 && (roll.Result + modifer >= AC))
            {
                cuttingwords--;
                modifer -= new Dice(10).Result;
            }
            if (luckpoints > 0 && roll.Result > 9 && (roll.Result + modifer >= AC))
            {
                luckpoints--;
                roll.Result = new List<Dice> { new Dice(20), roll }.OrderBy(x => x.Result).First().Result;
            }
            return (roll.Result + modifer >= AC);
        }

        public bool MakeSave(int saveDC)
        {
            throw new NotImplementedException();
        }
        public int mistystep = 3;
        public int discordentWhispers = 0;
        private bool enemyHasDisadvantage = false;

        public void TakeTurn(ICombatant enemy, Range range)
        {
            if (range.CurrentDistance > AttackRange)
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            if (range.CurrentDistance > AttackRange && range.CurrentDistance <= 320 && mistystep > 0)
            {
                MoveRelativeEnemy(AttackRange, range);
                mistystep--;
            }
            if (range.CurrentDistance > AttackRange)
            {
                MoveRelativeEnemy(AttackRange, range);
            }
            else
            {
                if (discordentWhispers > 0)
                {
                    int damage = DiscordDamage();
                    if (enemy.MakeSave(17))
                    {
                        enemy.CurrentHP -= damage / 2;
                    }
                    else
                    {
                        enemy.CurrentHP -= damage;
                        range.CurrentDistance += enemy.Movement;
                    }
                }
                else
                {
                    Dice D = new Dice(20);
                    if (enemy.DoesAttackHit(D, 9))
                    {
                        enemy.CurrentHP -= AttackDamage() + 4;
                        if (D.Result == 20) enemy.CurrentHP -= AttackDamage();
                        enemyHasDisadvantage = true;
                    }
                }
            }
        }

        private int DiscordDamage()
        {
            return new Dice(6).Result + new Dice(6).Result + new Dice(6).Result;
        }

        private int AttackDamage()
        {
            return new Dice(4).Result + new Dice(4).Result + new Dice(4).Result;
        }

        private void MoveRelativeEnemy(int TargetDistance, Range range)
        {
            range.CurrentDistance -= Math.Min(range.CurrentDistance - TargetDistance, 30);
        }
    }
    class FiddleSticksNoLuck : FiddleSticks
    {
        public FiddleSticksNoLuck() : base()
        {
            luckpoints = 0;
        }
    }
    class FiddleSticksNoCuttingWords : FiddleSticks
    {
        public FiddleSticksNoCuttingWords() : base()
        {
            cuttingwords = 0;
        }
    }
    class FiddleSticksNoNothing : FiddleSticks
    {
        public FiddleSticksNoNothing() : base()
        {
            cuttingwords = 0;
            luckpoints = 0;
        }
    }
    class FiddleSticksACUP : FiddleSticks
    {
        public FiddleSticksACUP() : base()
        {
            AC = 15;
        }

    }
    class FiddleSticksMAX : FiddleSticks
    {
        public FiddleSticksMAX() : base()
        {
            AC = 15;
            discordentWhispers = 4;
        }
    }
}
