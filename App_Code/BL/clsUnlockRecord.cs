using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;

/// <summary>
/// Summary description for clsUnlockRecord
/// </summary>
public class clsUnlockRecord
{

    public clsUnlockRecord()
    {

    }
    public static System.Timers.Timer aTimer;

    static void Main(string[] args)
    {
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += new System.Timers.ElapsedEventHandler(abc);

    }

    private static void abc(object src, ElapsedEventArgs e)
    {
        clsIsEdit.Tender_No = 9999;
    }

}