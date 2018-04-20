namespace TankGame.Messaging {

    public class ScoreChangedMessage : IMessage
    {
        public Score CurrenScore { get; private set; }

        public ScoreChangedMessage (Score score)
        {
            CurrenScore = score;
        }
    }
}
