using Accord.MachineLearning.Boosting;
using Accord.MachineLearning.Boosting.Learners;
using Accord.Statistics.Analysis;
using AngleSharp;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.VisualBasic.FileIO;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Models.Regression;

namespace MegaHockey
{
    public class KHLParser
    {
        public struct Game{
            public String first;
            public String second;
            public String date;

            public new String ToString => this.first + " " + this.second;
        }


        public static String[] Colors = new string[]
        {
            "#845EC2",
            "#D65DB1",
            "#FF6F91",
            "#FF6F91",
            "#FFC75F",
            "#F9F871",
            "#38BFB9",
            "#00ADCD",
            "#0095D6",
            "#6478C8",
            "#9355A2",
            "#A5316A",
            "#C60089",
            "#F13371",
            "#FF675A",
            "#FF9A4B",
            "#FFCA50",
            "#F9F871",
            "#D30000",
            "#C90050",
            "#9E277C",
            "#324C78",
            "#2F4858",
            "#845EC2",
        };



        public static List<string> TeamCodes = new List<String>
        {
            "Витязь",
            "Йокерит",
            "СКА",
            "ХК Сочи",
            "Спартак",
            "Торпедо НН",
            "Динамо Мн",
            "Динамо М",
            "Динамо Р",
            "Локомотив",
            "Северсталь",
            "Автомобилист",
            "Ак Барс",
            "Куньлунь РС",
            "Металлург Мг",
            "Нефтехимик",
            "Трактор",
            "Авангард",
            "Адмирал",
            "Амур",
            "Барыс",
            "Салават Юлаев",
            "Сибирь",
            "ЦСКА",
        };

        public static Dictionary<String, String> Trainers = new Dictionary<string, string>()
        {
            {"Витязь", "Юрий Бабенко"},
            {"Йокерит", "Лаури Марьямяки"},
            {"СКА", "Роман Ротенберг"},
            {"ХК Сочи", "Андрей Назаров"},
            {"Спартак", "Борис Миронов"},
            {"Торпедо НН", "Немировски Дэвид"},
            {"Динамо Мн", "Вудкрофт Крэйг"},
            {"Динамо М", "Кудашов Алексей"},
            {"Динамо Р", "Крикунов Владимир"},
            {"Локомотив", "Никитин Игорь"},
            {"Северсталь", "Разин Андрей"},
            {"Автомобилист", "Заварухин Николай"},
            {"Ак Барс", "Квартальнов Дмитрий"},
            {"Куньлунь РС", "Занатта Иванo"},
            {"Металлург Мг", "Воробьёв Илья"},
            {"Нефтехимик", "Леонтьев Олег"},
            {"Трактор", "Гатиятулин Анвар"},
            {"Авангард", "Хартли Боб"},
            {"Адмирал", "Тамбиев Леонид"},
            {"Амур", "Кравец Михаил"},
            {"Барыс", "Михайлис Юрий"},
            {"Салават Юлаев", "Лямся Томи"},
            {"Сибирь", "Мартемьянов Андрей"},
            {"ЦСКА", "Фёдоров Сергей"},
        };


        public static readonly Dictionary<String, String> Aliases = new Dictionary<string, string>()
        {
            {"Витязь", "vityaz"},
            {"Йокерит", "jokerit"},
            {"СКА", "ska"},
            {"ХК Сочи", "hc_sochi"},
            {"Спартак", "spartak"},
            {"Торпедо НН", "torpedo"},
            {"Динамо Мн", "dinamo_mn"},
            {"Динамо М", "dynamo_msk"},
            {"Динамо Р", "dinamo_r"},
            {"Локомотив", "lokomotiv"},
            {"Северсталь", "severstal"},
            {"Автомобилист", "avtomobilist"},
            {"Ак Барс", "ak_bars"},
            {"Куньлунь РC", "kunlun"},
            {"Металлург Мг", "metallurg_mg"},
            {"Нефтехимик", "neftekhimik"},
            {"Трактор", "traktor"},
            {"Авангард", "avangard"},
            {"Адмирал", "admiral"},
            {"Амур", "amur"},
            {"Барыс", "barys"},
            {"Салават Юлаев", "salavat_yulaev"},
            {"Сибирь", "sibir"},
            {"ЦСКА", "cska"},
        };
        private static string KHL_BASEPATH = "https://www.khl.ru/clubs/";
        public static readonly Dictionary<String, String> Avatars = new Dictionary<string, string>()
        {
            {"Витязь", "https://www.khl.ru/images/teams/ru/1097/19"},
            {"Йокерит", "https://www.khl.ru/images/teams/ru/1097/450"},
            {"СКА", "https://www.khl.ru/images/teams/ru/1097/24"},
            {"ХК Сочи", "https://www.khl.ru/images/teams/ru/1097/451"},
            {"Спартак", "https://www.khl.ru/images/teams/ru/1097/7"},
            {"Торпедо НН", "https://www.khl.ru/images/teams/ru/1097/26"},
            {"Динамо Мн", "https://www.khl.ru/images/teams/ru/1097/207"},
            {"Динамо М", "https://www.khl.ru/images/teams/ru/1097/719"},
            {"Динамо Р", "https://www.khl.ru/images/teams/ru/1097/208"},
            {"Локомотив", "https://www.khl.ru/images/teams/ru/1097/1"},
            {"Северсталь", "https://www.khl.ru/images/teams/ru/1097/56"},
            {"Автомобилист", "https://www.khl.ru/images/teams/ru/1097/190"},
            {"Ак Барс", "https://www.khl.ru/images/teams/ru/1097/53"},
            {"Куньлунь РС", "https://www.khl.ru/images/teams/ru/1097/568"},
            {"Металлург Мг", "https://www.khl.ru/images/teams/ru/1097/37"},
            {"Нефтехимик", "https://www.khl.ru/images/teams/ru/1097/71"},
            {"Трактор", "https://www.khl.ru/images/teams/ru/1097/25"},
            {"Авангард", "https://www.khl.ru/images/teams/ru/1097/34"},
            {"Адмирал", "https://www.khl.ru/images/teams/ru/1097/418"},
            {"Амур", "https://www.khl.ru/images/teams/ru/1097/54"},
            {"Барыс", "https://www.khl.ru/images/teams/ru/1097/198"},
            {"Салават Юлаев", "https://www.khl.ru/images/teams/ru/1097/38"},
            {"Сибирь", "https://www.khl.ru/images/teams/ru/1097/29"},
            {"ЦСКА", "https://www.khl.ru/images/teams/ru/1046/2"},
        };


        public static async Task<List<Game>> GetTeamGamesAsync(String team) 
        {
            
            if(!KHLParser.Aliases.ContainsKey(team)) throw new NoNearGamesFoundException();
            var teamcode = KHLParser.Aliases[team];

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(KHLParser.KHL_BASEPATH + teamcode);


            var left = document.QuerySelector(".b-half_block");
            var gamelist = left?.QuerySelector("ul.b-wide_tile")?.Children;
            if (gamelist == null)
            {
                throw new NoNearGamesFoundException();
            }

            List<Game> games = new List<Game>();
            
            foreach(var game in gamelist)
            {
                var teams = game.QuerySelectorAll(".m-club .e-club_name a");
                games.Add(new Game()
                {
                    first = teams[0].TextContent,
                    second = teams[1].TextContent,
                    date = game.QuerySelector(".e-match-num")?.TextContent,
            });
            }
            return games;
        }

        public static string GetTeamAvatar(String team) {
            if(!Avatars.ContainsKey(team)){
                return "https://i1.sndcdn.com/avatars-000411292317-dt2f28-t500x500.jpg";
                //throw new AvatarNotFoundException();
            }
             return Avatars[team];
           
        }   
        
        public static (List<double[]>, List<int>, List<int>) GamesHistory()
        {
            var pathToGamesData = "hockeygamesdata.csv";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            List<double[]> teams = new List<double[]>();

            List<int> first = new List<int>();
            List<int> second = new List<int>();

            using (TextFieldParser csvParser = new TextFieldParser(pathToGamesData, Encoding.UTF8))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;


                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[]? fields = csvParser.ReadFields();
                    int one = KHLParser.TeamCodes.IndexOf(fields[0].Trim());
                    int two = KHLParser.TeamCodes.IndexOf(fields[1].Trim());
                    if (one == -1 || two == -1)
                    {
                        throw new Exception("Ошибка в названии  команды");
                    }
                    teams.Add(new double[]
                    {
                    one, two
                    });
                    first.Add(int.Parse(fields[2]));
                    second.Add(int.Parse(fields[3]));
                }

            }
            return (teams, first, second);
        }

        public static Boost<DecisionStump> GetLearned()
        {

            (List<double[]> teams, List<int> first, List<int> second) = GamesHistory();

            var learner = new AdaBoost<DecisionStump>()
            {
                Learner = (p) => new ThresholdLearning(),
                MaxIterations = 3,
                Tolerance = 1e-3
            };


            return learner.Learn(teams.ToArray(), first.Select((x, i) => x > second[i]).ToArray());
        }

        public static List<(string, string, int, int)> GetTeamStatistics(string team)
        {
            List<(string, string, int, int)> results = new List<(string, string, int, int)> ();

            var teamcode = KHLParser.TeamCodes.IndexOf(team);
            if (teamcode == -1)
            {
                throw new Exception("Неверное название команды");
            }
            var history = GamesHistory();
            for (int i = 0; i < teamcode; i++)
            {
                if (history.Item1[i][0] == teamcode)
                {
                    results.Add((KHLParser.TeamCodes[(int)history.Item1[i][0]], KHLParser.TeamCodes[(int)history.Item1[i][1]], history.Item2[i], history.Item3[i]));
                }
            }
            return results;
        }

        public static List<(string, string, int, int)> GetPairStatistics(string one, string two)
        {
            List<(string, string, bool)> result = new List<(string, string, bool)>();

            var onecode = KHLParser.TeamCodes.IndexOf(one);
            var twocode = KHLParser.TeamCodes.IndexOf(two);
            if (onecode == -1  ||  twocode == -1)
            {
                throw new Exception("Неверное название команды");
            }


            (var games, var first, var second) = GamesHistory();

            var stats = games.Select((x, i) => (x, first[i], second[i])).Where((x) =>
            {
                (var game, var first, var second) = x;
                if (game[0] == onecode && game[1] == twocode)
                {
                    return true;
                }
                if (game[1] == onecode && game[0] == twocode)
                {
                    return true;
                }
                return false;
            });


            return stats.Select(y =>
            {
                (var game, var first, var second) = y;
                return (KHLParser.TeamCodes[(int)game[0]], KHLParser.TeamCodes[(int)game[1]], first, second);
            }).ToList();
        }




    }





}
