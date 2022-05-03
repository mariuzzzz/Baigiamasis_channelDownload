using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Web_Ataskaitos
{
    public partial class RDS : Form
    {
        public RDS()
        {
            InitializeComponent();
        }
        private void RDS_Load(object sender, EventArgs e)
        {
            string api_key = "X2vHCFDUxfu4yPHrddpsGDjkrm68zH7perATLVDN";
            string url = "c0983.w.dedikuoti.lt/api/live";
            string json = (new WebClient()).DownloadString(url + "&api_key=" + api_key);
            webTextData.Text = "ddddd";
        }
    }
}
