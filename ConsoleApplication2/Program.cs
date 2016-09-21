using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numSims = 100000;
            int startRange = 120;

            Trace.Listeners.Clear();

            TextWriterTraceListener twtl = new TextWriterTraceListener(Directory.GetCurrentDirectory()+"\\"+startRange.ToString()+"range.csv");
            twtl.Name = "TextLogger";
            twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

            ConsoleTraceListener ctl = new ConsoleTraceListener(false);
            ctl.TraceOutputOptions = TraceOptions.DateTime;

            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;

            Trace.WriteLine("The Following contains simplified combat simulations against Giant Dwarf Arbelest. Each PC variation will fight " + numSims.ToString() + " time going first and going second at a range of " + startRange + ". the PC variations will use various ammounts of their feats and ablities as described in there sections.");
            Trace.WriteLine("FiddleSticks. using Visious Mockery; Cutting Words; Luck; Misty Step,Not using Dissonant Whispers");
            Trace.WriteLine(",,Remaining Resources");
            Trace.WriteLine("Initative,Survival Rate,Average HP,Average Cutting Words,Average Luck,Misty Steps,Dissonant Whispers if used");

            var results = CombatSimulator.SimulateFights(numSims, startRange, new FiddleSticks(), new GiantDwarf());

            WriteFiddleOutput(results,"First",numSims,startRange);

            results = CombatSimulator.SimulateFights(numSims, startRange, new GiantDwarf(), new FiddleSticks());

            WriteFiddleOutput(results, "Second", numSims, startRange);
        }
        static void WriteFiddleOutput(List<Result> results, string Initative, int numSims, int startRange)
        {
            //Trace.WriteLine(",,Remaining Resources");
            //Trace.WriteLine("Initative,Survival Rate,Average HP,Average Cutting Words,Average Luck,Misty Steps,Dissonant Whispers if used");
            List<FiddleSticks> winsInType = new List<FiddleSticks>();
            List<Result> winsInResult = results.Where(x => x.winner.GetType().IsAssignableFrom(typeof(FiddleSticks))).ToList();
            winsInResult.ForEach(x=> winsInType.Add((FiddleSticks)x.winner));
            double survivalRate = Math.Round((double)winsInType.Count() / (numSims / 100),3);
            double hpRemaining = Math.Round((double)winsInType.Sum(x => x.CurrentHP) / winsInType.Count(),3);
            double cuttingWords = Math.Round((double)winsInType.Sum(x => x.cuttingwords) / winsInType.Count(),3);
            double luck = Math.Round((double)winsInType.Sum(x => x.luckpoints) / winsInType.Count(),3);
            double misty = Math.Round((double)winsInType.Sum(x => x.mistystep) / winsInType.Count(),3);
            double whispers = Math.Round((double)winsInType.Sum(x => x.discordentWhispers) / winsInType.Count(),3);
            Trace.WriteLine(Initative + ","+survivalRate.ToString()+"%,"+hpRemaining + "," +cuttingWords.ToString() + "," +luck.ToString() + "," +misty.ToString() + "," +whispers.ToString());
        }
    }
}
