using System.Threading.Tasks;

namespace LS_Diabetes_App.Interfaces
{
    public interface IDialog
    {
        Task<bool> AlertAsync(string Title, string Message, string Positif, string Negatif);
    }
}