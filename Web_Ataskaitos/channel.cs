using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Web_Ataskaitos
{
    public class channel
    {
        public static string id, show_id;
        DBaccess dBaccess = new DBaccess();
        DataTable dtShowInfo = new DataTable();


        public void channel_Load()
        {
            SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Ataskaitos;Integrated Security=True");
            con.Open();
            SqlCommand getTable = new SqlCommand("select * from channel_list", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(getTable);
            dataAdapter.Fill(dtShowInfo);
            int channelNumber = dtShowInfo.Rows.Count;
            string channel_id = String.Empty;
            string channelName = String.Empty;
            for (int j = 1; j <= channelNumber; j++)
            {
                SqlCommand getCommand = new SqlCommand("select ChannelID from channel_list where ID = '" + j + "'", con);
                SqlCommand getNameCommand = new SqlCommand("select ChannelName from channel_list where ID = '" + j + "'", con);
                getCommand.Parameters.AddWithValue("@j", @j);
                SqlDataReader da = getCommand.ExecuteReader();
                while (da.Read())
                {
                    channel_id = da.GetValue(0).ToString();
                }
                da.Close();
                getNameCommand.Parameters.AddWithValue("@j", @j);
                SqlDataReader dn = getNameCommand.ExecuteReader();
                while (dn.Read())
                {
                    channelName = dn.GetValue(0).ToString();
                }
                dn.Close();
                Encoding encoding = Encoding.UTF8;
                string json = (new WebClient()).DownloadString("URL not shown for security reasons" + channel_id + "&secondsAgo=259200");
                var match = Regex.Match(json, @"\{(.|\s)*\}");
                string test = match.Value;
                Model model = JsonConvert.DeserializeObject<Model>(match.Value);
                List<Datum> datum = model.data;
                
                string deltaStart = String.Empty;
                string channelId = String.Empty;
                string timeStart = String.Empty;
                string timeStop = String.Empty;
                string title = String.Empty;
                string deltaStop = String.Empty;
                string showId = String.Empty;
                string moderStatusStart = String.Empty;
                string moderStatusStop = String.Empty;
                string errorCodeStart = String.Empty;
                string errorCodeStop = String.Empty;

                for (int i = 0; i < model.data.Count; i++)
                {
                    if (datum[i].title.engl != null)
                    {
                        title = datum[i].title.engl;
                    }
                    else if (datum[i].title.slvk != null)
                    {
                        title = datum[i].title.slvk;
                    }
                    else if (datum[i].title.dutc != null)
                    {
                        title = datum[i].title.dutc;
                    }
                    else
                    {
                        title = datum[i].title.czch;
                    }
                    byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(title);
                    title = Encoding.ASCII.GetString(bytes);
                    deltaStart = datum[i].delta_start;
                    channelId = datum[i].channel_id;
                    timeStart = Convert.ToDateTime(datum[i].time_start).ToString();
                    timeStop = Convert.ToDateTime(datum[i].time_stop).ToString();
                    deltaStop = datum[i].delta_stop;
                    showId = datum[i].id;
                    moderStatusStart = datum[i].moder_status_start;
                    moderStatusStop = datum[i].moder_status_stop;
                    errorCodeStart = GetStartErrorCode(moderStatusStart);
                    errorCodeStop = GetStopErrorCode(moderStatusStop);



                    string querry = "select * from show_details where show_id = '" + showId + "'";
                    //string deleteQ = "delete from show_details where time_start < DATEADD(day, -2, GETDATE())";
                    string deleteQ = "delete from show_details where time_start <= GETDATE()";
                    SqlCommand select = new SqlCommand(querry, con);
                    // con.Open();
                    
                    SqlDataReader reader =  select.ExecuteReader();

                    try
                    {

                        if (reader.HasRows)
                        {
                            dBaccess.closeConn();

                            SqlCommand updateCommand = new SqlCommand("update show_details set channel_ID = @channelId, show_id = @showId, time_start = @timeStart, time_stop = @timeStop, title = @title, delta_start = @deltaStart, delta_stop = @deltaStop, moder_status_start = @moderStatusStart, moder_status_stop = @moderStatusStop, channel_name = @channelName, start_error_type = @errorCodeStart, stop_error_type = @errorCodeStop where show_id = '" + showId + "' ");
                            updateCommand.Parameters.AddWithValue("@channelId", @channelId);
                            updateCommand.Parameters.AddWithValue("@showId", @showId);
                            updateCommand.Parameters.AddWithValue("@timeStart", @timeStart);
                            updateCommand.Parameters.AddWithValue("@timeStop", @timeStop);
                            updateCommand.Parameters.AddWithValue("@title", @title);
                            updateCommand.Parameters.AddWithValue("@channelName", @channelName);
                            if (string.IsNullOrEmpty(deltaStart))
                                updateCommand.Parameters.Add("@deltaStart", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@deltaStart", @deltaStart);
                            if (string.IsNullOrEmpty(deltaStop))
                                updateCommand.Parameters.Add("@deltaStop", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@deltaStop", @deltaStop);
                            if (string.IsNullOrEmpty(moderStatusStart))
                                updateCommand.Parameters.Add("@moderStatusStart", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@moderStatusStart", @moderStatusStart);
                            if (string.IsNullOrEmpty(moderStatusStop))
                                updateCommand.Parameters.Add("@moderStatusStop", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@moderStatusStop", @moderStatusStop);
                            if (string.IsNullOrEmpty(errorCodeStart))
                                updateCommand.Parameters.Add("@errorCodeStart", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@errorCodeStart", @errorCodeStart);
                            if (string.IsNullOrEmpty(errorCodeStop))
                                updateCommand.Parameters.Add("@errorCodeStop", DBNull.Value);
                            else
                                updateCommand.Parameters.Add("@errorCodeStop", @errorCodeStop);
                            dBaccess.executeQuery(updateCommand);
                        }
                        else
                        {
                            dBaccess.closeConn();
                            SqlCommand insertCommand = new SqlCommand("insert into show_details(channel_ID, show_id, time_start, time_stop, title, delta_start, delta_stop, moder_status_start, moder_status_stop, channel_name, start_error_type, stop_error_type) values(@channelId, @showId, @timeStart, @timeStop, @title, @deltaStart, @deltaStop, @moderStatusStart, @moderStatusStop, @channelName, @errorCodeStart, @errorCodeStop)");
                            insertCommand.Parameters.AddWithValue("@channelId", @channelId);
                            insertCommand.Parameters.AddWithValue("@showId", @showId);
                            insertCommand.Parameters.AddWithValue("@timeStart", @timeStart);
                            insertCommand.Parameters.AddWithValue("@timeStop", @timeStop);
                            insertCommand.Parameters.AddWithValue("@title", @title);
                            insertCommand.Parameters.AddWithValue("@channelName", @channelName);
                            if (string.IsNullOrEmpty(deltaStart))
                                insertCommand.Parameters.Add("@deltaStart", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@deltaStart", @deltaStart);
                            if (string.IsNullOrEmpty(deltaStop))
                                insertCommand.Parameters.Add("@deltaStop", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@deltaStop", @deltaStop);
                            if (string.IsNullOrEmpty(moderStatusStart))
                                insertCommand.Parameters.Add("@moderStatusStart", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@moderStatusStart", @moderStatusStart);
                            if (string.IsNullOrEmpty(moderStatusStop))
                                insertCommand.Parameters.Add("@moderStatusStop", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@moderStatusStop", @moderStatusStop);
                            if (string.IsNullOrEmpty(errorCodeStart))
                                insertCommand.Parameters.Add("@errorCodeStart", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@errorCodeStart", @errorCodeStart);
                            if (string.IsNullOrEmpty(errorCodeStop))
                                insertCommand.Parameters.Add("@errorCodeStop", DBNull.Value);
                            else
                                insertCommand.Parameters.Add("@errorCodeStop", @errorCodeStop);
                            dBaccess.executeQuery(insertCommand);
                        }
                    }
                    finally
                    {

                        dBaccess.closeConn();
                        reader.Close();
                    }
                    // Delete if too much information
                   /* SqlCommand deleteCommand = new SqlCommand(deleteQ);
                    deleteCommand.Parameters.AddWithValue("@channelId", @channelId);
                    deleteCommand.Parameters.AddWithValue("@showId", @showId);
                    deleteCommand.Parameters.AddWithValue("@timeStart", @timeStart);
                    deleteCommand.Parameters.AddWithValue("@timeStop", @timeStop);
                    deleteCommand.Parameters.AddWithValue("@title", @title);
                    deleteCommand.Parameters.AddWithValue("@channelName", channelName);
                    if (string.IsNullOrEmpty(deltaStart))
                        deleteCommand.Parameters.Add("@deltaStart", DBNull.Value);
                    else
                        deleteCommand.Parameters.Add("@deltaStart", @deltaStart);
                    if (string.IsNullOrEmpty(deltaStop))
                        deleteCommand.Parameters.Add("@deltaStop", DBNull.Value);
                    else
                        deleteCommand.Parameters.Add("@deltaStop", @deltaStop);
                    if (string.IsNullOrEmpty(moderStatusStart))
                        deleteCommand.Parameters.Add("@moderStatusStart", DBNull.Value);                    
                    else
                        deleteCommand.Parameters.Add("@moderStatusStart", @moderStatusStart);
                    if (string.IsNullOrEmpty(moderStatusStop))
                        deleteCommand.Parameters.Add("@moderStatusStop", DBNull.Value);
                    else
                        deleteCommand.Parameters.Add("@moderStatusStop", @moderStatusStop);
                    dBaccess.executeQuery(deleteCommand);*/
                }
                
            }
        }
        public string GetStartErrorCode(string moderStatusStart)
        {
            switch (moderStatusStart)
            {
                case "1":
                    return "Additional Event ID";
                case "2":
                    return "Excluded Event ID";
                case "3":
                    return "Schedule inaccurate";
                case "4":
                    return "Interupted Sat signal";
                case "5":
                    return "Accurate time delayed";
                case "6":
                    return "Grey screens";
                default:
                    return "";
            }
        }
        public string GetStopErrorCode(string moderStatusStop)
        {
            switch (moderStatusStop)
            {
                case "1":
                    return "Additional Event ID";
                case "2":
                    return "Excluded Event ID";
                case "3":
                    return "Schedule inaccurate";
                case "4":
                    return "Interupted Sat signal";
                case "5":
                    return "Accurate time delayed";
                case "6":
                    return "Grey screens";
                default:
                    return "";
            }
        }
    }
}
