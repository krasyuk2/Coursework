using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DataBase : MonoBehaviour
{
   private FormReg _formReg;
   private SqliteConnection _conn;
   private DisplayText _displayText;

   private void Awake()
   {
      _displayText = FindObjectOfType<DisplayText>();
   }

   private void Start()
   {
      ConnectedDataBase(@"D:\\DbBase\DataBaseGame.db");
   }

   public bool ConnectedDataBase(string path)
   {
      _conn = new SqliteConnection("Data Source = " + path);
      _conn.Open();
      return true;
   }

   public bool Disconnect()
   {
      if (_conn != null)
      {
         if (_conn.State == ConnectionState.Open)
         {
            _conn.Dispose();
         }
      }
      return true;
   }
   public bool AddNewUser(string login, string password)
   {
      if (_conn != null)
      {
         if (_conn.State == ConnectionState.Open)
         {
            try
            {
               if (login.Length > 1 && password.Length > 1)
               {
                  SqliteCommand cmd = _conn.CreateCommand();
                  cmd.CommandText = "SELECT * FROM users WHERE login = '" + login + "'";
                  object res = cmd.ExecuteScalar();
                  if (res != null)
                  {
                     print("Login уже существует");
                  }
                  else
                  {
                     cmd.CommandText = "INSERT INTO users ('login', 'password') VALUES ('" + login + "', '" +
                                       GetHash(password) + "')";
                     cmd.ExecuteNonQuery();
                     return true;
                  }
               }
               else
               {
                  print("Пустые значения");
                  _displayText.Message("Пустые значения");
               }
            }
            catch (Exception e)
            {
              print(e.Message);
            }
          
         }
      }
      return false;
   }
   public bool AuthUser(string login, string password)
   {
      if (_conn != null)
      {
         if (_conn.State == ConnectionState.Open)
         {
            try
            {
               SqliteCommand cmd = _conn.CreateCommand();
               cmd.CommandText = "SELECT * FROM users WHERE login = '" + login + "' and password = '" +
                                 GetHash(password) + "'";
               object res = cmd.ExecuteScalar();
               if (res != null)
               {
                  print("Нашел");
                  return true;
               }
               return false;
            }
            catch (Exception e)
            {
               print(e.Message);
            }
          
         }
      }

      return false;
   }

   public bool DetectedLogin(string login)
   {
      SqliteCommand cmd = _conn.CreateCommand();
      cmd.CommandText = "SELECT * FROM users WHERE login = '" + login + "'";
      object res = cmd.ExecuteScalar();
      if (res != null)
      {
         return true;
      }

      return false;
   }

   public bool DetectedSpecialSymbols(string text)
   {
      if (text.Contains("-") || text.Contains("\"") || text.Contains("'") || text.Contains(" ")) return true;
      return false;
   }

   string GetHash(string text)
   {
      SHA256 sha256 = SHA256.Create();
      byte[] raw_text = Encoding.Unicode.GetBytes(text);
      byte[] raw_hash = sha256.ComputeHash((raw_text));
      string hash = Encoding.Unicode.GetString(raw_hash);
      sha256.Clear();
      return hash;

   }

   
}
