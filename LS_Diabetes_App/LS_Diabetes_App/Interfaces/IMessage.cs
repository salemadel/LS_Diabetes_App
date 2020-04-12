using System;
using System.Collections.Generic;
using System.Text;

namespace LS_Diabetes_App.Interfaces
{
    public interface IMessage
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
