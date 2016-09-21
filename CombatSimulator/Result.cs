namespace CombatSimulator
{
    public class Result
    {
        public int roundsTaken;
        public ICombatant winner;
        public int currentDistance;
        public ICombatant loser;
        public Result(ICombatant winner, ICombatant loser, int roundsTaken, int currentDistance)
        {
            this.winner = winner;
            this.loser = loser;
            this.roundsTaken = roundsTaken;
            this.currentDistance = currentDistance;
        }
    }
}