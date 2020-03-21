using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS_Diabetes_App.Interfaces
{
    public class DataStorecs : IDataStore
    {

        private SQLiteConnection _connection;
        public  DataStorecs(IDatabaseAccess db)
        {
            _connection = new SQLiteConnection(db.DatabasePath());
            _connection.CreateTable<Data_Model>();
            _connection.CreateTable<Glucose_Model>();
        }
        public void   AddData(Data_Model data)
        {
             _connection.Insert(data);
            System.Diagnostics.Debug.WriteLine("rani 3gabt men");
        }

        public void AddGlucose(Glucose_Model data)
        {
            _connection.Insert(data);
        }

        public void   DeleteData(Data_Model data)
        {
             _connection.Delete(data);
        }

        public  Data_Model GetData(int id)
        {
            return  _connection.Find<Data_Model>(id);
        }

        public  IEnumerable<Glucose_Model> GetGlucosAsync()
        {
            return  _connection.Query<Glucose_Model>("Select * From Glucose_Model ");
        }

        public void  UpdateData(Data_Model data)
        {
             _connection.Update(data);
        }
    }
}
