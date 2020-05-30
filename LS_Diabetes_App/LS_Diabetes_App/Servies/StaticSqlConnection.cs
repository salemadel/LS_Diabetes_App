using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LS_Diabetes_App.Servies
{
    public static class StaticSqlConnection
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection GetConnection()
        {
            if(_connection == null)
            {
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LS_Diabets_App_Db");

                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                _connection.CreateTable<Data_Model>();
                _connection.CreateTable<Settings_Model>();
                _connection.CreateTable<Glucose_Model>();
                _connection.CreateTable<Pression_Model>();
                _connection.CreateTable<Weight_Model>();
                _connection.CreateTable<Drugs_Model>();
                _connection.CreateTable<Insulune_Model>();
                _connection.CreateTable<Hb1Ac_Model>();
                _connection.CreateTable<Profil_Model>();
                _connection.CreateTable<Objectif_Model>();
                _connection.CreateTable<Drugs_Model>();
                _connection.CreateTable<Steps_Model>();
            }
            return _connection;
        }
    }
}
