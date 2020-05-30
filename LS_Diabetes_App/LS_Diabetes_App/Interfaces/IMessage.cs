namespace LS_Diabetes_App.Interfaces
{
    public interface IMessage
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}