using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for clsNoToWord
/// </summary>
public class clsNoToWord
{

    //variables
    static Int32[] lvalue = new int[31];
    static string[] lword = new string[31];
    static Int32 i = 0;
    static double k = 0.00;
    static Int32 j = 0;
    static string cgtword = "";
    static double ln = 0.00;
    static string lw = "";
    static string lreturn = "";
    static double calResult = 0.00;
    static string tme = "";



    public clsNoToWord()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ctgword(string llword)
    {
        try
        {
            ln = 0.00;

            lreturn = "";
            lvalue[0] = 1; lword[0] = "One";
            lvalue[1] = 2; lword[1] = "Two";
            lvalue[2] = 3; lword[2] = "Three";
            lvalue[3] = 4; lword[3] = "Four";
            lvalue[4] = 5; lword[4] = "Five";
            lvalue[5] = 6; lword[5] = "Six";
            lvalue[6] = 7; lword[6] = "Seven";
            lvalue[7] = 8; lword[7] = "Eight";
            lvalue[8] = 9; lword[8] = "Nine";
            lvalue[9] = 10; lword[9] = "Ten";
            lvalue[10] = 11; lword[10] = "Eleven";
            lvalue[11] = 12; lword[11] = "Twelve";
            lvalue[12] = 13; lword[12] = "Thirteen";
            lvalue[13] = 14; lword[13] = "Fourteen";
            lvalue[14] = 15; lword[14] = "Fifteen";
            lvalue[15] = 16; lword[15] = "Sixteen";
            lvalue[16] = 17; lword[16] = "Seventeen";
            lvalue[17] = 18; lword[17] = "Eighteen";
            lvalue[18] = 19; lword[18] = "Ninteen";
            lvalue[19] = 20; lword[19] = "Twenty";
            lvalue[20] = 30; lword[20] = "Thirty";
            lvalue[21] = 40; lword[21] = "Fourty";
            lvalue[22] = 50; lword[22] = "Fifty";
            lvalue[23] = 60; lword[23] = "Sixty";
            lvalue[24] = 70; lword[24] = "Seventy";
            lvalue[25] = 80; lword[25] = "Eighty";
            lvalue[26] = 90; lword[26] = "Ninty";
            lvalue[27] = 100; lword[27] = "Hundred";
            lvalue[28] = 1000; lword[28] = "Thousand";
            lvalue[29] = 100000; lword[29] = "Lac";
            lvalue[30] = 10000000; lword[30] = "Crore";

            //Crore
            if (Convert.ToDouble(llword) >= 10000000)
            {
                k = Convert.ToDouble(llword) / 10000000;
                k = Math.Floor(k);
                ln = ln + (k * 10000000);

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lw + " Crore";
                }
            }

            //Lac
            if (Convert.ToDouble(llword) >= 100000)
            {
                k = (Convert.ToDouble(llword) - ln) / 100000;
                k = Math.Floor(k);
                ln = ln + (k * 100000);

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Lac";
                }
            }
            //Thousand
            if (Convert.ToDouble(llword) >= 1000)
            {
                k = (Convert.ToDouble(llword) - ln) / 1000;

                k = Math.Floor(k);
                ln = (k * 1000) + ln;

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Thousand";
                }
            }
            //Hundred
            if (Convert.ToDouble(llword) >= 100)
            {
                k = (Convert.ToDouble(llword) - ln) / 100;
                k = Math.Floor(k);
                ln = (k * 100) + ln;

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Hundred";
                }
            }

            //Rs
            if (Convert.ToDouble(llword) >= 0)
            {
                k = (Convert.ToDouble(llword) - ln) / 1;
                k = Math.Floor(k);
                ln = (k * 1) + ln;

                if (k > 0)
                {
                    lw = numToWord(k);

                    if (lw != "")
                    {
                        lreturn = lreturn + " " + lw;
                    }
                }
            }

            //paise
            k = Math.Round((Convert.ToDouble(llword) - Math.Floor(Convert.ToDouble(llword))) * 100, 2);

            if (k > 0)
            {
                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " and " + lw + " Paise ";
                }
            }
            lreturn = lreturn + " Only.";

            return lreturn;
        }

        catch
        {
            return "";
        }
    }

    private static string numToWord(double k)
    {
        lw = "";


        if (k <= 19)
        {
            k = Math.Floor(k);
            if (k != 0)
            {
                lw = lword[Convert.ToInt32(k) - 1];
            }
        }
        else
        {
            j = Convert.ToInt32(Math.Floor(k / 10) * 10);
            for (int i = 20; i < 30; i++)
            {
                if (lvalue[i - 1] == j)
                    lw = lw + lword[i - 1];
            }

            j = Convert.ToInt32(Math.Floor(mod(k, 10)));
            if (j != 0)
                lw = lw + " " + lword[j - 1];
            else
                lw = lw;


        }
        return lw;
    }

    private static double mod(double a, int n)
    {
        double result = a % n;
        if ((a < 0 && n > 0) || (a > 0 && n < 0))
            result += n;
        result = Math.Floor(result);
        return result;
    }

    public void WriteToFile(string text)
    {
        string path = "D:\\error.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            writer.Close();
        }
    }
}

public class clsNoToWordEuro
{

    //variables
    static Int32[] lvalue = new int[31];
    static string[] lword = new string[31];
    static Int32 i = 0;
    static double k = 0.00;
    static Int32 j = 0;
    static string cgtword = "";
    static double ln = 0.00;
    static string lw = "";
    static string lreturn = "";
    static double calResult = 0.00;
    static string tme = "";



    public clsNoToWordEuro()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ctgword(string llword)
    {
        try
        {
            ln = 0.00;

            lreturn = "";
            lvalue[0] = 1; lword[0] = "One";
            lvalue[1] = 2; lword[1] = "Two";
            lvalue[2] = 3; lword[2] = "Three";
            lvalue[3] = 4; lword[3] = "Four";
            lvalue[4] = 5; lword[4] = "Five";
            lvalue[5] = 6; lword[5] = "Six";
            lvalue[6] = 7; lword[6] = "Seven";
            lvalue[7] = 8; lword[7] = "Eight";
            lvalue[8] = 9; lword[8] = "Nine";
            lvalue[9] = 10; lword[9] = "Ten";
            lvalue[10] = 11; lword[10] = "Eleven";
            lvalue[11] = 12; lword[11] = "Twelve";
            lvalue[12] = 13; lword[12] = "Thirteen";
            lvalue[13] = 14; lword[13] = "Fourteen";
            lvalue[14] = 15; lword[14] = "Fifteen";
            lvalue[15] = 16; lword[15] = "Sixteen";
            lvalue[16] = 17; lword[16] = "Seventeen";
            lvalue[17] = 18; lword[17] = "Eighteen";
            lvalue[18] = 19; lword[18] = "Ninteen";
            lvalue[19] = 20; lword[19] = "Twenty";
            lvalue[20] = 30; lword[20] = "Thirty";
            lvalue[21] = 40; lword[21] = "Fourty";
            lvalue[22] = 50; lword[22] = "Fifty";
            lvalue[23] = 60; lword[23] = "Sixty";
            lvalue[24] = 70; lword[24] = "Seventy";
            lvalue[25] = 80; lword[25] = "Eighty";
            lvalue[26] = 90; lword[26] = "Ninty";
            lvalue[27] = 100; lword[27] = "Hundred";
            lvalue[28] = 1000; lword[28] = "Thousand";
            lvalue[29] = 100000; lword[29] = "Lac";
            lvalue[30] = 10000000; lword[30] = "Crore";

            //Crore
            if (Convert.ToDouble(llword) >= 10000000)
            {
                k = Convert.ToDouble(llword) / 10000000;
                k = Math.Floor(k);
                ln = ln + (k * 10000000);

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lw + " Crore";
                }
            }

            //Lac
            if (Convert.ToDouble(llword) >= 100000)
            {
                k = (Convert.ToDouble(llword) - ln) / 100000;
                k = Math.Floor(k);
                ln = ln + (k * 100000);

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Lac";
                }
            }
            //Thousand
            if (Convert.ToDouble(llword) >= 1000)
            {
                k = (Convert.ToDouble(llword) - ln) / 1000;

                k = Math.Floor(k);
                ln = (k * 1000) + ln;

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Thousand";
                }
            }
            //Hundred
            if (Convert.ToDouble(llword) >= 100)
            {
                k = (Convert.ToDouble(llword) - ln) / 100;
                k = Math.Floor(k);
                ln = (k * 100) + ln;

                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " " + lw + " Hundred";
                }
            }

            //Rs
            if (Convert.ToDouble(llword) >= 0)
            {
                k = (Convert.ToDouble(llword) - ln) / 1;
                k = Math.Floor(k);
                ln = (k * 1) + ln;

                if (k > 0)
                {
                    lw = numToWord(k);

                    if (lw != "")
                    {
                        lreturn = lreturn + " " + lw;
                    }
                }
            }

            //paise
            k = Math.Round((Convert.ToDouble(llword) - Math.Floor(Convert.ToDouble(llword))) * 100, 2);

            if (k > 0)
            {
                lw = numToWord(k);
                if (lw != "")
                {
                    lreturn = lreturn + " and " + lw + " Cents ";
                }
            }
            lreturn = lreturn + " Only.";

            return lreturn;
        }

        catch
        {
            return "";
        }
    }

    private static string numToWord(double k)
    {
        lw = "";


        if (k <= 19)
        {
            k = Math.Floor(k);
            if (k != 0)
            {
                lw = lword[Convert.ToInt32(k) - 1];
            }
        }
        else
        {
            j = Convert.ToInt32(Math.Floor(k / 10) * 10);
            for (int i = 20; i < 30; i++)
            {
                if (lvalue[i - 1] == j)
                    lw = lw + lword[i - 1];
            }

            j = Convert.ToInt32(Math.Floor(mod(k, 10)));
            if (j != 0)
                lw = lw + " " + lword[j - 1];
            else
                lw = lw;


        }
        return lw;
    }

    private static double mod(double a, int n)
    {
        double result = a % n;
        if ((a < 0 && n > 0) || (a > 0 && n < 0))
            result += n;
        result = Math.Floor(result);
        return result;
    }

    public void WriteToFile(string text)
    {
        string path = "D:\\error.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            writer.Close();
        }
    }
}

