using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Rollback
/// </summary>
public class Rollback
{
	public Rollback()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  /*public string SubmitDailyAccountMaster(int DailyAcctNo, ArrayList ArrDailyAct, ArrayList ArrDailyNominee, ArrayList ArrJointAcct, string OperationType)
    {
        SocietyDAO sc = new SocietyDAO();
        sc = (SocietyDAO)HttpContext.Current.Session["Society"];
        BranchDAO br = new BranchDAO();
        br = (BranchDAO)HttpContext.Current.Session["Branch"];

        string Result = "";
        SqlTransaction tran = null;

        conn.Open();
        tran = conn.BeginTransaction();
        try
        {
            //-----------------insert into member master table---------------
            DailyAcctDAO D = new DailyAcctDAO();
            D = (DailyAcctDAO)ArrDailyAct[0];
            SqlCommand cmd = new SqlCommand("SSP_Submit_DailyAccountMst", conn);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SocietyNo", SqlDbType.Int).Value = sc.gblint_SocietyNo;
            cmd.Parameters.Add("@BranchNo", SqlDbType.Int).Value = br.gblint_BranchNo;
            cmd.Parameters.Add("@Memberno", SqlDbType.Int).Value = D.MemberNo;
            cmd.Parameters.Add("@Gl_acct_code", SqlDbType.Int).Value = D.Gl_acct_Code;
            cmd.Parameters.Add("@Daily_account_no", SqlDbType.Int).Value = DailyAcctNo;
            cmd.Parameters.Add("@LFNo", SqlDbType.NVarChar).Value = D.LFNo;
            cmd.Parameters.Add("@Agent_id", SqlDbType.Int).Value = D.AgentId;
            cmd.Parameters.Add("@Start_date", SqlDbType.NVarChar).Value = Common.getDBDate(D.StartDate);
            cmd.Parameters.Add("@Period", SqlDbType.NVarChar).Value = D.Period;
            cmd.Parameters.Add("@Period_uom", SqlDbType.NVarChar).Value = D.PeriodType;
            cmd.Parameters.Add("@Interest_rate", SqlDbType.Decimal).Value = D.IntRate;
            cmd.Parameters.Add("@Maturity_date", SqlDbType.NVarChar).Value = Common.getDBDate(D.MaturityDate);
            cmd.Parameters.Add("@Daily_deposit_amt_daily", SqlDbType.Decimal).Value = D.DailyAmount;
            cmd.Parameters.Add("@Maturity_amt", SqlDbType.Decimal).Value = D.MaturityAmount;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = D.Description;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = D.Status;
            cmd.Parameters.Add("@Joint_ac_holder_YN", SqlDbType.NVarChar).Value = D.JointAcct;
            cmd.Parameters.Add("@Guardian_YN", SqlDbType.NVarChar).Value = D.Guardian;
            cmd.Parameters.Add("@Operation_mode", SqlDbType.NVarChar).Value = D.ModeOfOperation;
            cmd.Parameters.Add("@OperationType", SqlDbType.NVarChar).Value = OperationType;

            cmd.ExecuteNonQuery();

            //---------------------------------insert into nominee details table---------------------------
            cmd = new SqlCommand("SSP_Submit_DailyAcctNomineeMst", conn);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Societyno", SqlDbType.Int).Value = sc.gblint_SocietyNo;
            cmd.Parameters.Add("@Branchno", SqlDbType.Int).Value = br.gblint_BranchNo;
            cmd.Parameters.Add("@Gl_acct_code", SqlDbType.Int).Value = D.Gl_acct_Code;
            cmd.Parameters.Add("@Daily_account_no", SqlDbType.Int).Value = DailyAcctNo;
            cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Delete";

            cmd.ExecuteNonQuery();

            if (ArrDailyNominee.Count > 0)
            {
                DailyAcctNomineeDAO Nobj = new DailyAcctNomineeDAO();
                for (int index = 0; index <= ArrDailyNominee.Count - 1; index++)
                {
                    Nobj = (DailyAcctNomineeDAO)ArrDailyNominee[index];
                    cmd = new SqlCommand("SSP_Submit_DailyAcctNomineeMst", conn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Societyno", SqlDbType.Int).Value = sc.gblint_SocietyNo;
                    cmd.Parameters.Add("@Branchno", SqlDbType.Int).Value = br.gblint_BranchNo;
                    cmd.Parameters.Add("@Memberno", SqlDbType.Int).Value = Nobj.MemberNo;
                    cmd.Parameters.Add("@Nomineeno", SqlDbType.Int).Value = Nobj.NomineeNo;
                    cmd.Parameters.Add("@Gl_acct_code", SqlDbType.Int).Value = Nobj.Gl_acct_Code;
                    cmd.Parameters.Add("@Daily_account_no", SqlDbType.Int).Value = DailyAcctNo;
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar).Value = Nobj.Prefix;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Nobj.Name;
                    cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = Nobj.Gender;
                    cmd.Parameters.Add("@Relation", SqlDbType.NVarChar).Value = Nobj.Relation;
                    cmd.Parameters.Add("@Birth_date", SqlDbType.NVarChar).Value = Common.getDBDate(Nobj.DOB);
                    cmd.Parameters.Add("@Occupation", SqlDbType.VarChar).Value = Nobj.Occupation;
                    cmd.Parameters.Add("@N_address", SqlDbType.NVarChar).Value = Nobj.Address;
                    cmd.Parameters.Add("@N_state", SqlDbType.NVarChar).Value = Nobj.State;
                    cmd.Parameters.Add("@N_district", SqlDbType.NVarChar).Value = Nobj.District;
                    cmd.Parameters.Add("@N_taluka", SqlDbType.NVarChar).Value = Nobj.Taluka;
                    cmd.Parameters.Add("@N_village", SqlDbType.NVarChar).Value = Nobj.Village;
                    cmd.Parameters.Add("@N_PIN", SqlDbType.NVarChar).Value = Nobj.Pin;
                    cmd.Parameters.Add("@N_landmark", SqlDbType.NVarChar).Value = Nobj.Landmark;
                    cmd.Parameters.Add("@N_STD", SqlDbType.NVarChar).Value = Nobj.TelephoneSTD;
                    cmd.Parameters.Add("@N_Telephone", SqlDbType.NVarChar).Value = Nobj.TelephoneNo;
                    cmd.Parameters.Add("@N_Mobile_R", SqlDbType.NVarChar).Value = Nobj.MobileR;
                    cmd.Parameters.Add("@N_Mobile_O", SqlDbType.NVarChar).Value = Nobj.MobileO;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Insert";

                    cmd.ExecuteNonQuery();
                }
            }
            //--------------------insert into service table---------------
            cmd = new SqlCommand("SSP_Submit_DailyJointAccountMst", conn);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Societyno", SqlDbType.Int).Value = sc.gblint_SocietyNo;
            cmd.Parameters.Add("@BranchNo", SqlDbType.Int).Value = br.gblint_BranchNo;
            cmd.Parameters.Add("@Gl_acct_code", SqlDbType.Int).Value = D.Gl_acct_Code;
            cmd.Parameters.Add("@Daily_account_no", SqlDbType.Int).Value = DailyAcctNo;
            cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Delete";
            cmd.ExecuteNonQuery();

            if (ArrJointAcct.Count > 0)
            {
                DailyJointAcctDAO DJ = new DailyJointAcctDAO();
                for (int index = 0; index <= ArrJointAcct.Count - 1; index++)
                {
                    DJ = (DailyJointAcctDAO)ArrJointAcct[index];

                    cmd = new SqlCommand("SSP_Submit_DailyJointAccountMst", conn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Societyno", SqlDbType.Int).Value = sc.gblint_SocietyNo;
                    cmd.Parameters.Add("@BranchNo", SqlDbType.Int).Value = br.gblint_BranchNo;
                    cmd.Parameters.Add("@Gl_acct_code", SqlDbType.Int).Value = DJ.Gl_acct_Code;
                    cmd.Parameters.Add("@Daily_account_no", SqlDbType.Int).Value = DailyAcctNo;
                    cmd.Parameters.Add("@Memberno", SqlDbType.Int).Value = DJ.MemberNo;
                    cmd.Parameters.Add("@Joint_memberno", SqlDbType.Int).Value = DJ.JointMemberNo;
                    cmd.Parameters.Add("@Photo", SqlDbType.NVarChar).Value = DJ.Photopath;
                    cmd.Parameters.Add("@Signature", SqlDbType.NVarChar).Value = DJ.SignPath;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = "Insert";

                    cmd.ExecuteNonQuery();
                }
            }
            tran.Commit();
            Result = "success";
        }
        catch (Exception ex)
        {
            Result = "Error";
            ex.Message.ToString();
            tran.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return Result;
    }*/
}