using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class GiantDwarf : ICombatant
    {
        private int Ammo = 12;
        public int AC
        {
            get
            {
                if (Ammo < 7)
                {
                    return 20 - (6 - Ammo);
                }
                else return 20;
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
        private string _name = "Arblest Dwarf";
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
                return 30;
            }
        }

        public int StartHP
        {
            get
            {
                return 52;
            }
        }
        public GiantDwarf()
        {
            CurrentHP = StartHP;
        }
        public bool DoesAttackHit(Dice roll, int modifier)
        {
            return (roll.Result + modifier >= AC);
        }

        public bool MakeSave(int saveDC)
        {
            return (new Dice(20).Result >= saveDC);
        }

        public void TakeTurn(ICombatant enemy, Range range)
        {
            if (Ammo > 1)
            {
                if (range.CurrentDistance > 320)//If outside normal range use movement to close
                {
                    MoveRelativeEnemy(320, range);
                }
                if (range.CurrentDistance > 320)//If outside max range use action to dash
                {
                    MoveRelativeEnemy(320, range);
                }
                else//Else Attack
                {
                    Ammo -= 2;
                    Dice diceroll;
                    if (range.CurrentDistance > 120)//Disadvantage for range
                    {
                        diceroll = new List<Dice> { new Dice(20), new Dice(20) }.OrderBy(x => x.Result).First();
                        if (enemy.DoesAttackHit(diceroll, 6))
                        {
                            enemy.CurrentHP -= AttackDamage();
                            if (diceroll.Result == 20) enemy.CurrentHP -= AttackDamage();
                        }
                        diceroll = new List<Dice> { new Dice(20), new Dice(20) }.OrderBy(x => x.Result).First();
                        if (enemy.DoesAttackHit(diceroll, 6))
                        {
                            enemy.CurrentHP -= AttackDamage();
                            if (diceroll.Result == 20) enemy.CurrentHP -= AttackDamage();
                        }
                    }
                    else
                    {
                        diceroll = new Dice(20);
                        if (enemy.DoesAttackHit(diceroll, 6))
                        {
                            enemy.CurrentHP -= AttackDamage();
                            if (diceroll.Result == 20) enemy.CurrentHP -= AttackDamage();
                        }
                        diceroll = new Dice(20);
                        if (enemy.DoesAttackHit(diceroll, 6))
                        {
                            enemy.CurrentHP -= AttackDamage();
                            if (diceroll.Result == 20) enemy.CurrentHP -= AttackDamage();
                        }

                    }
                }
            }
            else range.CurrentDistance += 60;//If we are out of ammo leg it!
        }
        private void MoveRelativeEnemy(int TargetDistance, Range range)
        {
            range.CurrentDistance -= Math.Min(range.CurrentDistance - TargetDistance, 30);
        }
        private int AttackDamage()
        {
            return new Dice(6).Result + new Dice(6).Result + new Dice(6).Result + new Dice(6).Result + new Dice(6).Result + new Dice(6).Result;
        }
    }
}
