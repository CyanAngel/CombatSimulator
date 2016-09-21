using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int numSims = 100000;
            int startRange = 420;

            Trace.Listeners.Clear();

            TextWriterTraceListener twtl = new TextWriterTraceListener(Directory.GetCurrentDirectory()+"\\"+startRange.ToString()+"range.csv");
            twtl.Name = "TextLogger";
            twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

            ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            ctl.TraceOutputOptions = TraceOptions.DateTime;

            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;

            //Trace.WriteLine("The Following contains simplified combat simulations against Giant Dwarf Arbelest. Each PC variation will fight " + numSims.ToString() + " time going first and going second at a range of " + startRange + ". the PC variations will use various ammounts of their feats and ablities as described in there sections.");
            
            Trace.WriteLine(",,,Remaining Resources");
            Trace.WriteLine(",Initative,Survival Rate,Average HP,HP%,Cutting Words,Luck,Misty Steps,Dissonant Whispers");
            Trace.Write("FiddleSticks. using Visious Mockery; Cutting Words; Luck; Misty Step; Not using Dissonant Whispers");

            var results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticks(), new GiantDwarf());

            WriteFiddleOutput(results,"First",numSims,startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticks());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.Write("FiddleSticks. using Visious Mockery; Luck; Misty Step; Not using Dissonant Whispers; Cutting Words;");

            results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticksNoCuttingWords(), new GiantDwarf());

            WriteFiddleOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticksNoCuttingWords());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.Write("FiddleSticks. using Visious Mockery; Cutting Words; Misty Step; Not using Dissonant Whispers; Luck;");

            results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticksNoLuck(), new GiantDwarf());

            WriteFiddleOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticksNoLuck());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.Write("FiddleSticks. using Visious Mockery; Misty Step; Not using Dissonant Whispers; Luck; Cutting Words;");

            results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticksNoNothing(), new GiantDwarf());

            WriteFiddleOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticksNoNothing());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.Write("FiddleSticks. using Visious Mockery; Misty Step; Luck; Cutting Words; AC15; Not using Dissonant Whispers");

            results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticksACUP(), new GiantDwarf());

            WriteFiddleOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticksACUP());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.Write("FiddleSticks. using Visious Mockery; Misty Step; Luck; Cutting Words; AC15; Dissonant Whispers");

            results = CombatSimulation.SimulateFights(numSims, startRange, new FiddleSticksMAX(), new GiantDwarf());

            WriteFiddleOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticksMAX());

            WriteFiddleOutput(results, "Second", numSims, startRange);

            Trace.WriteLine(",Initative,Survival Rate,Average HP,HP%");
            Trace.Write("Xaphriel On Foot");

            results = CombatSimulation.SimulateFights(numSims, startRange, new Xaphriel(), new GiantDwarf());

            WriteXaphOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new Xaphriel());

            WriteXaphOutput(results, "Second", numSims, startRange);

            Trace.Write("Xaphriel On Horse");

            results = CombatSimulation.SimulateFights(numSims, startRange, new XaphrielHorse(), new GiantDwarf());

            WriteXaphOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new XaphrielHorse());

            WriteXaphOutput(results, "Second", numSims, startRange);

            Trace.WriteLine(",Initative,Survival Rate,Average HP,HP%,Shield,SP,Misty Steps,Fireballs");
            Trace.Write("Thia No Shields; Quickening or Fireballs");

            results = CombatSimulation.SimulateFights(numSims, startRange, new ThiaNoNothing(), new GiantDwarf());

            WriteThiaOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new ThiaNoNothing());

            WriteThiaOutput(results, "Second", numSims, startRange);

            Trace.Write("Thia No Quickening or Fireballs");

            results = CombatSimulation.SimulateFights(numSims, startRange, new ThiaNoQuickeningOrFireballs(), new GiantDwarf());

            WriteThiaOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new ThiaNoQuickeningOrFireballs());

            WriteThiaOutput(results, "Second", numSims, startRange);

            Trace.Write("Thia No Fireballs");

            results = CombatSimulation.SimulateFights(numSims, startRange, new ThiaNoFireBalls(), new GiantDwarf());

            WriteThiaOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new ThiaNoFireBalls());

            WriteThiaOutput(results, "Second", numSims, startRange);

            Trace.Write("Thia No Quickening");

            results = CombatSimulation.SimulateFights(numSims, startRange, new ThiaNoQuickening(), new GiantDwarf());

            WriteThiaOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new ThiaNoQuickening());

            WriteThiaOutput(results, "Second", numSims, startRange);

            Trace.Write("Thia Unlimited");

            results = CombatSimulation.SimulateFights(numSims, startRange, new Thia(), new GiantDwarf());

            WriteThiaOutput(results, "First", numSims, startRange);

            results = CombatSimulation.SimulateFights(numSims, startRange, new GiantDwarf(), new Thia());

            WriteThiaOutput(results, "Second", numSims, startRange);
        }
        static void WriteFiddleOutput(List<Result> results, string Initative, int numSims, int startRange)
        {
            //Trace.WriteLine(",,Remaining Resources");
            //Trace.WriteLine("Initative,Survival Rate,Average HP,Average Cutting Words,Average Luck,Misty Steps,Dissonant Whispers if used");
            List<FiddleSticks> winsInType = new List<FiddleSticks>();
            List<Result> winsInResult = results.Where(x => x.winner is FiddleSticks).ToList();
            winsInResult.ForEach(x=> winsInType.Add((FiddleSticks)x.winner));
            double survivalRate = Math.Round((double)winsInType.Count() / (numSims / 100),3);
            double hpRemaining = Math.Round((double)winsInType.Sum(x => x.CurrentHP) / winsInType.Count(),3);
            double hpPercent = Math.Round((hpRemaining / winsInType.FirstOrDefault().StartHP) * 100, 3);
            double cuttingWords = Math.Round((double)winsInType.Sum(x => x.cuttingwords) / winsInType.Count(),3);
            double luck = Math.Round((double)winsInType.Sum(x => x.luckpoints) / winsInType.Count(),3);
            double misty = Math.Round((double)winsInType.Sum(x => x.mistystep) / winsInType.Count(),3);
            double whispers = Math.Round((double)winsInType.Sum(x => x.discordentWhispers) / winsInType.Count(),3);
            Trace.WriteLine("," + Initative + ","+survivalRate.ToString()+"%,"+hpRemaining + "," + hpPercent + "%," + cuttingWords.ToString() + "," +luck.ToString() + "," +misty.ToString() + "," +whispers.ToString());
        }
        static void WriteXaphOutput(List<Result> results, string Initative, int numSims, int startRange)
        {
            List<Xaphriel> winsInType = new List<Xaphriel>();
            List<Result> winsInResult = results.Where(x => x.winner is Xaphriel).ToList();
            winsInResult.ForEach(x => winsInType.Add((Xaphriel)x.winner));
            double survivalRate = Math.Round((double)winsInType.Count() / (numSims / 100), 3);
            double hpRemaining = Math.Round((double)winsInType.Sum(x => x.CurrentHP) / winsInType.Count(), 3);
            double hpPercent = Math.Round((hpRemaining / winsInType.FirstOrDefault().StartHP) * 100, 3);         
            Trace.WriteLine("," + Initative + "," + survivalRate.ToString() + "%," + hpRemaining + "," + hpPercent + "%");
        }
        static void WriteThiaOutput(List<Result> results, string Initative, int numSims, int startRange)
        {
            //Trace.WriteLine(",,Remaining Resources");
            //Trace.WriteLine("Initative,Survival Rate,Average HP,HP%,shield,misty step,SP,Fireballs");
            List<Thia> winsInType = new List<Thia>();
            List<Result> winsInResult = results.Where(x => x.winner is Thia).ToList();
            winsInResult.ForEach(x => winsInType.Add((Thia)x.winner));
            double survivalRate = Math.Round((double)winsInType.Count() / (numSims / 100), 3);
            double hpRemaining = Math.Round((double)winsInType.Sum(x => x.CurrentHP) / winsInType.Count(), 3);
            double hpPercent = Math.Round((hpRemaining / winsInType.FirstOrDefault().StartHP) * 100, 3);
            double shield = Math.Round((double)winsInType.Sum(x => x.sheildsLeft) / winsInType.Count(), 3);
            double sp = Math.Round((double)winsInType.Sum(x => x.SP) / winsInType.Count(), 3);
            double misty = Math.Round((double)winsInType.Sum(x => x.mistySteps) / winsInType.Count(), 3);
            double fireball = Math.Round((double)winsInType.Sum(x => x.fireballs) / winsInType.Count(), 3);
            Trace.WriteLine("," + Initative + "," + survivalRate.ToString() + "%," + hpRemaining + "," + hpPercent + "%," + shield.ToString() + "," + sp.ToString() + "," + misty.ToString() + "," + fireball.ToString());
        }
    }
}
