using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GeneratePassword
/// </summary>
public class GeneratePassword
{
    public GeneratePassword()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string NewPassword()
    {
        string PasswordLength = "6";
        //This one, is empty for now - but will ultimately hold the finised randomly generated password
        string NewPassword = "";

        //This one tells you which characters are allowed in this new password
        string allowedChars = "";
        allowedChars = "1,2,3,4,5,6,7,8,9,0";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        //allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        //allowedChars += "~,!,@,#,$,%,^,&,*,+,?";
        
        //Then working with an array...
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);
        string IDString = "";
        string temp = "";
        //utilize the "random" class
        Random rand = new Random();
        //and lastly - loop through the generation process...
        for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            IDString += temp;
            NewPassword = IDString;
            //For Testing purposes, I used a label on the front end to show me the generated password.
            //lblProduct.Text = IDString;
        }
        return NewPassword;
    }
}