using Accord.MachineLearning.Boosting;
using Accord.MachineLearning.Boosting.Learners;
using Accord.Statistics.Models.Regression;

namespace MegaHockey
{
    public class ClassifierSingletone
    {
        private ClassifierSingletone()
        {
            this.classifier = KHLParser.GetLearned();
            this.GamesHistory = KHLParser.GamesHistory();
            this.Wins = getGameHistory();
            this.SortedWinners = this.SortWinners();
        }

        public Boost<DecisionStump> classifier { get; private set; }


        public int[] Wins { get; private set; }
        public (List<double[]>, List<int>, List<int>) GamesHistory { get; private set; }

        public List<(string, int)> SortedWinners { get; private set; }


        private static ClassifierSingletone instance = null;

        public static ClassifierSingletone Instance()
        {
            if (ClassifierSingletone.instance == null)
            {
                instance = new ClassifierSingletone();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        private int[] getGameHistory()
        {
            int[] res  = new int[KHLParser.TeamCodes.Count];

            (var teams, var l, var r) = this.GamesHistory;

            for (var i = 0; i<teams.Count; i++)
            {
                var teaml = teams[i][0];
                var teamr = teams[i][1];

                if(l[i] > r[i])
                {
                    res[(int)teaml]++;
                }else
                {
                    res[(int)teamr]++;
                }
            }
            return res;

        }

        private List<(string, int)> SortWinners()
        {
            var arr = KHLParser.TeamCodes.Select((x, i) => (x, this.Wins[i]))
                .OrderByDescending(x => x.Item2).ToList();

            return arr;
        }


    }
}
