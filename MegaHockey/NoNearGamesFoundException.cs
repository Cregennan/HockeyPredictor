namespace MegaHockey
{
    public class NoNearGamesFoundException : Exception
    {
        public NoNearGamesFoundException() : base("У данной команды нет игр в ближайшее время")
        {
            
        }
    }
}
