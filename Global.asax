<%@ Application Language="C#" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //ThreadStart task = new ThreadStart(TaskLoop);
        //Thread MyTask = new Thread(task);
        //MyTask.Start();
    }
    public static void TaskLoop()
    { 
        // In this example, task will repeat in infinite loop
        // You can additional parameter if you want to have an option 
        // to stop the task from some page
        while (true)
        {
            // Execute scheduled task
            ScheduledTask();
            // Wait for certain time interval
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));
        }
    }

    static void ScheduledTask()
    {
        // Task code which is executed periodically
        //clsIsEdit.Tender_No = 9999;
        //string mobile = "8806091854";
        //string API = clsGV.msgAPI;
        //string Url = API + "mobile=" + mobile + "&message=Hello Ankush You Are Successfully Done this Code....:) Driver Mob:" + mobile;
        //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
        //HttpWebResponse resp = (HttpWebResponse)myReq.GetResponse();
        //StreamReader reder = new StreamReader(resp.GetResponseStream());
        //string respString = reder.ReadToEnd();
        //reder.Close();
        //resp.Close();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            Response.Redirect("../Sugar/pgeHome.aspx");
        }
        else
        {
            Session["lastPage"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("~/login1.aspx");
        }
        // Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
       
</script>
