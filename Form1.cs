using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProEWB.API;

namespace TaxProEWBApiIntigrationDemo
{
    public partial class Form1 : Form
    {
        public EWBSession EwbSession = new EWBSession();

        public Form1()
        {
            InitializeComponent();
            DisplayApiSettings();
            DisplayApiLoginDetails();
            EwbSession.RefreshAuthTokenCompleted += RefreshLoginDetailsDisplay;
        }

        private void RefreshLoginDetailsDisplay(object sender, EventArgs e)
        {
            DisplayApiLoginDetails();
        }

        private void DisplayApiSettings()
        {
            txtGSPName.Text = EwbSession.EwbApiSetting.GSPName;
            txtASPUserID.Text = EwbSession.EwbApiSetting.AspUserId;
            txtAspPassword.Text = EwbSession.EwbApiSetting.AspPassword;
            txtClientId.Text = EwbSession.EwbApiSetting.EWBClientId;
            txtClientSecret.Text = EwbSession.EwbApiSetting.EWBClientSecret;
            txtGspUserId.Text = EwbSession.EwbApiSetting.EWBGSPUserID;
            txtBaseURL.Text = EwbSession.EwbApiSetting.BaseUrl;
        }
        private void DisplayApiLoginDetails()
        {
            txtGstin.Text = EwbSession.EwbApiLoginDetails.EwbGstin;
            txtUserId.Text = EwbSession.EwbApiLoginDetails.EwbUserID;
            txtPassword.Text = EwbSession.EwbApiLoginDetails.EwbPassword;
            txtAppKey.Text = EwbSession.EwbApiLoginDetails.EwbAppKey;
            txtAuthToken.Text = EwbSession.EwbApiLoginDetails.EwbAuthToken;
            txtTokenExp.Text = EwbSession.EwbApiLoginDetails.EwbTokenExp.ToString("dd/MM/yyyy HH:mm:ss");
            txtSEK.Text = EwbSession.EwbApiLoginDetails.EwbSEK;
        }
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            EwbSession.EwbApiSetting.GSPName = txtGSPName.Text;
            EwbSession.EwbApiSetting.AspUserId = txtASPUserID.Text;
            EwbSession.EwbApiSetting.AspPassword = txtAspPassword.Text;
            EwbSession.EwbApiSetting.EWBClientId = txtClientId.Text;
            EwbSession.EwbApiSetting.EWBClientSecret = txtClientSecret.Text;
            EwbSession.EwbApiSetting.EWBGSPUserID = txtGspUserId.Text;
            EwbSession.EwbApiSetting.BaseUrl = txtBaseURL.Text;
            Shared.SaveAPISetting(EwbSession.EwbApiSetting);
            MessageBox.Show("Ewb ApiSetting Saved Successfully.");
        }
        private void btnSaveLoginDetails_Click(object sender, EventArgs e)
        {
            //string FileName = "EwbApiLoginDetails.json";
            EwbSession.EwbApiLoginDetails.EwbGstin = txtGstin.Text;
            EwbSession.EwbApiLoginDetails.EwbUserID = txtUserId.Text;
            EwbSession.EwbApiLoginDetails.EwbPassword = txtPassword.Text;
            EwbSession.EwbApiLoginDetails.EwbAppKey = txtAppKey.Text;
            EwbSession.EwbApiLoginDetails.EwbAuthToken = EwbSession.EwbApiLoginDetails.EwbAuthToken;
            EwbSession.EwbApiLoginDetails.EwbTokenExp = EwbSession.EwbApiLoginDetails.EwbTokenExp;
            EwbSession.EwbApiLoginDetails.EwbSEK = EwbSession.EwbApiLoginDetails.EwbSEK;
            Shared.SaveAPILoginDetails(EwbSession.EwbApiLoginDetails);
            MessageBox.Show("Ewb Ewb ApiLoginDetails Saved Successfully.");
        }

        private async void btnAuthToken_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Calling any API method will internally check for valid AuthToken and would try to obtain AuthToken if its is expired.  You don't need to explicitly call GetAuthTokenAsync method. Do you want to proceed?", "AuthToken is Automatic", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TxnRespWithObj<EWBSession> TxnResp = await EWBAPI.GetAuthTokenAsync(EwbSession);
                if (TxnResp.IsSuccess)
                {
                    //Call Refresh Display Api Login Details to refresh auth token and Exp Time in display
                    DisplayApiLoginDetails();
                }
                rtbResponce.Text = TxnResp.TxnOutcome;
            }
        }

        private async void btnGenEWB_Click(object sender, EventArgs e)
        {
            ReqGenEwbPl ewbGen = new ReqGenEwbPl();
            ewbGen.supplyType = "O";
            ewbGen.subSupplyType = "1";
            ewbGen.docType = "INV";
            ewbGen.docNo = "123-8";
            ewbGen.docDate = "15/12/2017";
            ewbGen.fromGstin = "08AAQPS7478C1Z1";
            ewbGen.fromTrdName = "welton";
           ewbGen.fromAddr1 = "2ND CROSS NO 59  19  A";
           ewbGen.fromAddr2 = "GROUND FLOOR OSBORNE ROAD";
           ewbGen.fromPlace = "FRAZER TOWN";
           ewbGen.fromPincode = 560042;
           ewbGen.fromStateCode = 29;
           ewbGen.toGstin = "05ABZPP6384Q1ZB";
           ewbGen.toTrdName = "sthuthya";
           ewbGen.toAddr1 = "Shree Nilaya";
           ewbGen.toAddr2 = "Dasarahosahalli";
           ewbGen.toPlace = "Beml Nagar";
           ewbGen.toPincode = 689788;
           ewbGen.toStateCode = 28;
           ewbGen.totalValue = 5609889;
           ewbGen.cgstValue = 0;
           ewbGen.sgstValue = 0;
           ewbGen.igstValue = 168296.67;
           ewbGen.cessValue = 224395.56;
           ewbGen.transporterId = "";
           ewbGen.transporterName = "";
           ewbGen.transDocNo = "";
           ewbGen.transMode = "1";
           ewbGen.transDistance = "656";
           ewbGen.transDocDate = "";
           ewbGen.vehicleNo = "PVC1234";
           ewbGen.vehicleType = "R";
           ewbGen.itemList = new List<ReqGenEwbPl.ItemList>();
           ewbGen.itemList.Add(new ReqGenEwbPl.ItemList
            {
               productName = "Wheat",
               productDesc = "Wheat",
               hsnCode = 1001,
               quantity = 4,
               qtyUnit = "BOX",
               cgstRate = 0,
               sgstRate = 0,
               igstRate = 3,
               cessRate = 4,
               cessAdvol = 0,
               taxableAmount = 5609889
            });
            
            TxnRespWithObj<RespGenEwbPl> TxnResp = await EWBAPI.GenEWBAsync(EwbSession, ewbGen);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }

        private async void btnUpdtVehicleNo_Click(object sender, EventArgs e)
        {
            ReqVehicleNoUpdtPl reqVehicleNo = new ReqVehicleNoUpdtPl();
            reqVehicleNo.EwbNo = 391000800056;
            reqVehicleNo.VehicleNo = "PQR1234";
            reqVehicleNo.FromPlace = "BANGALORE";
            reqVehicleNo.FromState = 29;
            reqVehicleNo.ReasonCode = "1";
            reqVehicleNo.ReasonRem = "vehicle broke down";
            reqVehicleNo.TransDocNo = "1234";
            reqVehicleNo.TransDocDate = "16/03/2018";
            reqVehicleNo.TransMode = "1";

           TxnRespWithObj<RespVehicleNoUpdtPl> resVehicleNoUpdt =  await EWBAPI.UpdateVehicleNosync(EwbSession, reqVehicleNo);

            if (resVehicleNoUpdt.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(resVehicleNoUpdt.RespObj);
            else
                rtbResponce.Text = resVehicleNoUpdt.TxnOutcome;

        }

        private async void btnGenerateCEWB_Click(object sender, EventArgs e)
        {
            ReqGenCEwbPl reqCEWB = new ReqGenCEwbPl();
            reqCEWB.fromPlace = "BANGALORE SOUTH";
            reqCEWB.fromState = "29";
            reqCEWB.vehicleNo = "KA12AB1234";
            reqCEWB.transMode = "1";
            reqCEWB.TransDocNo = "1234";
            reqCEWB.TransDocDate = "26/02/2018";
            reqCEWB.tripSheetEwbBills = new List<ReqGenCEwbPl.TripSheetEwbBills>();
            reqCEWB.tripSheetEwbBills.Add(new ReqGenCEwbPl.TripSheetEwbBills {
                ewbNo = 391000800056,
            });

            TxnRespWithObj<RespGenCEwbPl>respGenCEWB = await EWBAPI.GenCEWBAsync(EwbSession, reqCEWB);
            if (respGenCEWB.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(respGenCEWB.RespObj);
            else
                rtbResponce.Text = respGenCEWB.TxnOutcome;
        }

        private async void btnCancelEWB_Click(object sender, EventArgs e)
        {
            ReqCancelEwbPl reqCancelEWB = new ReqCancelEwbPl();
            reqCancelEWB.ewbNo = "451000613026";
            reqCancelEWB.cancelRsnCode = 2;
            reqCancelEWB.cancelRmrk = "Cancelled the order";

            TxnRespWithObj<RespCancelEwbPl>respCancelEWB = await EWBAPI.CancelEWBAsync(EwbSession, reqCancelEWB);
            if (respCancelEWB.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(respCancelEWB.RespObj);
            else
                rtbResponce.Text = respCancelEWB.TxnOutcome;
        }

        private async void btnRejectEWB_Click(object sender, EventArgs e)
        {
            ReqRejectEwbPl reqRejectEWB = new ReqRejectEwbPl();
            reqRejectEWB.ewbNo = "481000612981";
            TxnRespWithObj<RespRejectEwbPl> respRejectEWB = await EWBAPI.RejectEWBAsync(EwbSession, reqRejectEWB);
            if (respRejectEWB.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(respRejectEWB.RespObj);
            else
                rtbResponce.Text = respRejectEWB.TxnOutcome;
        }
        private async void btnGetEWBDetails_Click(object sender, EventArgs e)
        {
            string EwbNo = "391000800056";
            TxnRespWithObj<RespGetEWBDetail> TxnResp = await EWBAPI.GetEWBDetailAsync(EwbSession, EwbNo);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }
        private async void btnEWBAsignForTrans_Click(object sender, EventArgs e)
        {
            string Date = "01/03/2018";
            TxnRespWithObj<AssignedEWBItem> TxnResp = await EWBAPI.GetEWBAssignedForTransAsync(EwbSession, Date);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }

        private async void btnAsignedByGSTIN_Click(object sender, EventArgs e)
        {
          
            string genGSTIN = "08AABCW0619D1ZO";
            string Date = "01/03/2018 10:41:00 AM";
            TxnRespWithObj<AssignedEWBItem> TxnResp = await EWBAPI.GetEWBAssignedForTransByGstinAsync(EwbSession, Date, genGSTIN);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }

        private async void btnByOthParty_Click(object sender, EventArgs e)
        {
            string Date = "01/03/2018 10:41:00 AM";
            TxnRespWithObj<AssignedEWBItem> TxnResp = await EWBAPI.GetEWBOfOtherPartyAsync(EwbSession, Date);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }

        private async void btnGetCEWB_Click(object sender, EventArgs e)
        {
            string tripSheetNo = "3410001807";
            TxnRespWithObj<GetConsolidatedEWB> TxnResp = await EWBAPI.GetConsolidatedEWBAsync(EwbSession, tripSheetNo);
            if (TxnResp.IsSuccess)
                rtbResponce.Text = JsonConvert.SerializeObject(TxnResp.RespObj);
            else
                rtbResponce.Text = TxnResp.TxnOutcome;
        }

        private void btnShowSharedLists_Click(object sender, EventArgs e)
        {
            rtbResponce.Text = "Comboboxs below filled with Shared Lists available...";

            cmbStates.DataSource = SharedLists.StateCodeList;
            cmbStates.DisplayMember = "Description";
            cmbStates.ValueMember = "Code";

            cmbDocumentTypes.DataSource = SharedLists.DocumentTypes;
            cmbDocumentTypes.DisplayMember = "Description";
            cmbDocumentTypes.ValueMember = "Code";

            cmbSupplyTypes.DataSource = SharedLists.SupplyTypes;
            cmbSupplyTypes.DisplayMember = "Description";
            cmbSupplyTypes.ValueMember = "Code";

            cmbSubSupplyTypes.DataSource = SharedLists.SubSupplyTypes;
            cmbSubSupplyTypes.DisplayMember = "Description";
            cmbSubSupplyTypes.ValueMember = "Code";

            cmbTrnsportationMode.DataSource = SharedLists.TransportationModes;
            cmbTrnsportationMode.DisplayMember = "Description";
            cmbTrnsportationMode.ValueMember = "Code";

            cmbUnits.DataSource = SharedLists.UnitList;
            cmbUnits.DisplayMember = "Description";
            cmbUnits.ValueMember = "Code";

            cmbVUpdateReason.DataSource = SharedLists.VehicleUpdateReasonCodes;
            cmbVUpdateReason.DisplayMember = "Description";
            cmbVUpdateReason.ValueMember = "Code";
        }
    }
}
