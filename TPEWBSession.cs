using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxProEWB.API;

namespace TaxProEWBApiIntigrationDemo
{
    /// <summary>
    /// Use this class if you want to handle ApiLoginSetting and ApiSession for multiple TaxPayers in your application.
    /// </summary>
    public class TPEWBSession : EWBSession
    {
        public int TaxPayerID { get; set; }
        public TPEWBSession(int TPID)
        {
            TaxPayerID = TPID;
            //StartingApiTxn += Event Handler Methods;
            //StartingRefreshAuthToken += Event Handler before calling Authentication API after Exp time;
            RefreshAuthTokenCompleted += SaveNewAuthToken;

        }

        private void SaveNewAuthToken(object sender, EventArgs e)
        {
            //Write your code to save New AuthToken to DB, etc to TaxPayerID
           
        }

        public override void LogAPITxn(APITxnLogArgs e)
        {
            //base.LogAPITxn(e);
            //Write your code to Log API Txn
        }
    }
}
