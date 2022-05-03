using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Ataskaitos
{
    public class Title
    {
        public string dutc { get; set; }
        public string czch { get; set; }
        public string slvk { get; set; }
        public string engl { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string channel_id { get; set; }
        public DateTime time_start { get; set; }
        public DateTime time_stop { get; set; }
        public string unix_start { get; set; }
        public string unix_stop { get; set; }
        public Title title { get; set; }
        public string moder_status_start { get; set; }
        public string moder_set_start { get; set; }
        public string moder_status_stop { get; set; }
        public string moder_set_stop { get; set; }
        public string sys_outside_id { get; set; }
        public DateTime real_time_start { get; set; }
        public string real_unix_start { get; set; }
        public string delta_start { get; set; }
        public DateTime real_time_stop { get; set; }
        public string real_unix_stop { get; set; }
        public string delta_stop { get; set; }
    }

    public class Log
    {
        public double time { get; set; }
        public int success { get; set; }
        public int rows { get; set; }
        public int size { get; set; }
    }

    public class Model
    {
        public List<Datum> data { get; set; }
        public Log log { get; set; }
    }



}
