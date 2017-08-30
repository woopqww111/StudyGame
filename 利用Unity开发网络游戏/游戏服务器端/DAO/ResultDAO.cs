﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class ResultDAO
    {
        public Result GetResultByUserid(MySqlConnection conn, int userid)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from result where userid = @userid ", conn);
                cmd.Parameters.AddWithValue("userid", userid);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
               
                    int winCount = reader.GetInt32("wincount");
                    int totalCount = reader.GetInt32("totalcount");
                   Result result = new Result(id,userid,totalCount,winCount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userid, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetResultByUserid的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;
        }
    }
}
