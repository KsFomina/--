﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutomaticMarkup.Layout.DataBase
{
    public class BaseConnection
    {
        string connectionString = "Server=DESKTOP-T89823G\\SQLEXPRESS;Database=History; Integrated Security=True";

        SqlConnection connection;

        public void openConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {

                connection = new SqlConnection(connectionString);
                connection.Open();

            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnectionString()
        {
            return connection;
        }

        public DataTable getData()
        {
            return connection.GetSchema("history");
        }

        public void AddData(string name_file, DateTime time_create, DateTime data_create, byte[] file_marking, byte[] file_source)
        {
            // SQL-запрос для вставки данных
            string query = "INSERT INTO history (name_file, time_create, date_create, file_marking, file_source) " +
                "VALUES (@name_file, @time_create, @date_create, @file_marking, @file_source)";

                // Создание объекта команды
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавление параметров в команду для защиты от SQL-инъекций
                    command.Parameters.AddWithValue("@name_file", name_file);
                    command.Parameters.AddWithValue("@time_create", time_create);
                    command.Parameters.AddWithValue("@date_create", data_create);
                    command.Parameters.AddWithValue("@file_marking", file_marking);
                    command.Parameters.AddWithValue("@file_source", file_source);

                    //// Открытие соединения
                    //openConnection();

                    // Выполнение команды
                    int result = command.ExecuteNonQuery();

                    // Проверка результата
                    if (result < 0)
                        Console.WriteLine("Ошибка вставки данных в базу данных!");
                    else
                        Console.WriteLine("Запись успешно добавлена.");
                    closeConnection();
                }
            
        }

        //public DataTable GetRow(int id)
        //{
        //    // SQL-запрос для получения данных
        //    string query = "SELECT * FROM ВашаТаблица WHERE id = @id";

        //    // Создаем DataTable, в который будут загружены данные
        //    DataTable dataTable = new DataTable();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Добавляем параметр для защиты от SQL-инъекций
        //            command.Parameters.AddWithValue("@id", id);

        //            // Открываем соединение
        //            connection.Open();

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                // Загружаем данные в DataTable
        //                dataTable.Load(reader);
        //            }

        //            // Закрываем соединение
        //            connection.Close();
        //        } 
        //    }
        //        return dataTable;
           
        //}
    }
}