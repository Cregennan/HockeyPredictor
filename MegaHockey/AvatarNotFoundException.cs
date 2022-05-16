namespace MegaHockey
{
    public class AvatarNotFoundException : Exception
    {
        public AvatarNotFoundException(): base("Аватар  для данной команды  не найден")
        {

        }
    }
}
