using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace NubiAlert
{
    public partial class Form1 : Form
    {
       
        public class Alert_Data
        {
            public int sq;
            public String Regist_System;
            public String Regist_Type;
            public String From_sq;
            public String From_Eng_Name;
            public String From_Kor_Name;
            public String From_Email;
            public String From_Mobile;
            public String To_sq;
            public String To_Eng_Name;
            public String To_Kor_Name;
            public String To_Email;
            public String To_Mobile;
            public String CC_sq;
            public String CC_Eng_Name;
            public String CC_Kor_Name;
            public String CC_Email;
            public String CC_Mobile;
            public String BCC_sq;
            public String BCC_Eng_Name;
            public String BCC_Kor_Name;
            public String BCC_Email;
            public String BCC_Mobile;
            public int customer_sq;
            //public String to_email;
            public String Subject;
            public String Body;
            public String Method; // EMAIL / SMS
            public String HTML; // 1 = HTML 0 = TEXT
            public String Priority; // High/Low/Normal
            public DateTime Regist_Datetime;
            public DateTime Send_Datetime;
            public DateTime Sent_Datetime;
            public bool spam;

            public String smtp_id;
            public String smtp_pw;
        }

        public class Employee
        {
            public int sq;
            public String Kor_Name;
            public String Eng_Name;
            public String Email;
            public String Mobile;
        }
        

        //private const string QUERY_ALERT_DATA = "select top 1 * from alert where sent_datetime is null and send_datetime <= getdate() order by send_datetime";
        private const string QUERY_ALERT_DATA = "select top 1 * from alert where sent_datetime is null and (to_sq is not null or to_email is not null) and send_datetime <= getdate() and Regist_Type != 'SalesMail' order by send_datetime";
        private const string QUERY_ALERT_DATA_SalesMail = "select top 1 * from alert where sent_datetime is null and (to_sq is not null or to_email is not null) and send_datetime <= getdate() and Regist_Type = 'SalesMail' order by send_datetime";
        private const string QUERY_ALERT_DATA_Except_QuickQuery = "select top 1 * from alert where sent_datetime is null and (to_sq is not null or to_email is not null) and send_datetime <= getdate() and regist_type != 'QUICKQUERY' and Regist_Type != 'SalesMail' order by send_datetime";
        private const string QUERY_ALERT_DATA1 = "select top 1 * from alert where sq=102721";
        private const string QUERY_EMPLOYEE_DATA = "select * from employee where sq=";
        private const string CONNSTR = "Server=192.168.2.120,14333;database=nubicom;uid=nubicom;pwd=chltlsdkagh!2011";

        public Form1()
        {

            InitializeComponent();

            timer1.Interval = Convert.ToInt32(textBox_Interval.Text) * 1000;
            timer1.Start();
            button1.Text = "Stop!!";
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox_Interval.Text) * 1000;
            if (button1.Text == "Stop!!")
            {
                timer1.Stop();
                button1.Text = "Start!!";
            }
            else
            {
                timer1.Start();
                button1.Text = "Stop!!";
            }
           
        }


        public bool SendMail(Alert_Data get_data)
        {
            bool mailSendStatus = false;

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress(get_data.From_Email,get_data.From_Kor_Name);

                if (get_data.To_Email != null)
                {
                    get_data.To_Email = get_data.To_Email.Replace("\n", "");
                    get_data.To_Email = get_data.To_Email.Replace("\r", "");
                }
                if (get_data.CC_Email != null)
                {
                    get_data.CC_Email = get_data.CC_Email.Replace("\n", "");
                    get_data.CC_Email = get_data.CC_Email.Replace("\r", "");
                }
                if (get_data.BCC_Email != null)
                {
                    get_data.BCC_Email = get_data.BCC_Email.Replace("\n", "");
                    get_data.BCC_Email = get_data.BCC_Email.Replace("\r", "");
                }

                if (get_data.Regist_Type != "Remittance")
                {
                    
                    String[] To_Email = get_data.To_Email.Split('|');
                    String[] To_Kor_Name = get_data.To_Kor_Name.Split('|');
                    String[] To_Eng_Name = get_data.To_Eng_Name.Split('|');
                    for (int i = 0; i < To_Email.Length; i++)
                    {
                        if (To_Email[i] != "")
                        {
                            To_Email[i] = To_Email[i].Replace("\n", "");
                            message.To.Add(new MailAddress(To_Email[i], To_Kor_Name[i]));
                        }
                    }
                    message.Subject = "[" + get_data.Regist_System + "]" + get_data.Subject;
                    if (get_data.Priority == "Normal")
                        message.Priority = System.Net.Mail.MailPriority.Normal;
                    else if (get_data.Priority == "High")
                        message.Priority = System.Net.Mail.MailPriority.High;
                    else if (get_data.Priority == "Low")
                        message.Priority = System.Net.Mail.MailPriority.Low;

                    if (get_data.HTML == "1")
                        message.IsBodyHtml = true;
                    else
                        message.IsBodyHtml = false;

                    message.Sender = new MailAddress(get_data.From_Email, get_data.From_Kor_Name);
                    if (get_data.CC_Email != null)
                    {
                        String[] CC_Email = get_data.CC_Email.Split('|');
                        String[] CC_Kor_Name = get_data.CC_Kor_Name.Split('|');
                        String[] CC_Eng_Name = get_data.CC_Eng_Name.Split('|');
                        for (int i = 0; i < CC_Email.Length; i++)
                        {
                            if (CC_Email[i] != "")
                            {
                                message.CC.Add(new MailAddress(CC_Email[i], CC_Kor_Name[i]));
                            }
                        }
                    }
                    if (get_data.BCC_Email != null)
                    {
                        String[] BCC_Email = get_data.BCC_Email.Split('|');
                        String[] BCC_Kor_Name = get_data.BCC_Kor_Name.Split('|');
                        String[] BCC_Eng_Name = get_data.BCC_Eng_Name.Split('|');
                        for (int i = 0; i < BCC_Email.Length; i++)
                        {
                            if (BCC_Email[i] != "")
                            {
                                message.Bcc.Add(new MailAddress(BCC_Email[i], BCC_Kor_Name[i]));
                            }
                        }
                    }
                }
                else
                {


                    String[] To_Email = get_data.To_Email.Split('|');
                    for (int i = 0; i < To_Email.Length; i++)
                    {
                        if (To_Email[i] != "")
                        {
                            message.To.Add(new MailAddress(To_Email[i], ""));
                        }
                    }

                    message.Subject = get_data.Subject;
                    //message.Subject = "[" + get_data.Regist_System + "]" + get_data.Subject;
                    if (get_data.Priority == "Normal")
                        message.Priority = System.Net.Mail.MailPriority.Normal;
                    else if (get_data.Priority == "High")
                        message.Priority = System.Net.Mail.MailPriority.High;
                    else if (get_data.Priority == "Low")
                        message.Priority = System.Net.Mail.MailPriority.Low;

                    if (get_data.HTML == "1")
                        message.IsBodyHtml = true;
                    else
                        message.IsBodyHtml = false;

                    message.Sender = new MailAddress(get_data.From_Email, get_data.From_Kor_Name);

                    if (get_data.CC_Email != null)
                    {
                        String[] CC_Email = get_data.CC_Email.Split('|');
                        String[] CC_Kor_Name = get_data.CC_Kor_Name.Split('|');
                        String[] CC_Eng_Name = get_data.CC_Eng_Name.Split('|');
                        for (int i = 0; i < CC_Email.Length; i++)
                        {
                            if (CC_Email[i] != "")
                            {
                                message.CC.Add(new MailAddress(CC_Email[i], CC_Kor_Name[i]));
                            }
                        }
                    }
                }
                message.Body = get_data.Body;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (get_data.Regist_Type == "Remittance")
                    smtp.Credentials = new System.Net.NetworkCredential("jyhan@nubicom.co.kr", "tkfkdgo66");   // 수금 요청메일은 한대리 이름으로 보냄
                else if (get_data.Regist_Type == "Repair alarm")
                    smtp.Credentials = new System.Net.NetworkCredential("bekim@nubicom.co.kr", "rlaqhdms1");
                else if (get_data.smtp_id.Trim() != "")
                    smtp.Credentials = new System.Net.NetworkCredential(get_data.smtp_id, get_data.smtp_pw);    // 영업사원 고객향 메일 발송
                else
                    smtp.Credentials = new System.Net.NetworkCredential("goodbug@nubicom.co.kr", "gytjql11");
                

                smtp.Send(message);
                mailSendStatus = true;
            }
            catch (Exception e)
            {
                mailSendStatus = false;
                Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "  Send Fail!!! --> " + get_data.From_Kor_Name + " -> " + get_data.To_Kor_Name + ":" + get_data.Subject + "\n" + e.Message);
            }
            return mailSendStatus;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox_Interval.Text) * 1000;
            try
            {
                Alert_Data get_data = Get_Data();

                if (!get_data.spam)
                {
                    if (get_data.Method == "EMAIL")
                    {
                        bool result = SendMail(get_data);
                        if (result)
                        {
                            update_alert(get_data.sq);
                            Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "  " + get_data.From_Kor_Name + " -> " + get_data.To_Kor_Name + ":" + get_data.Subject);
                        }

                    }
                    else if (get_data.Method == "SMS")
                    {
                        // SMS 발송루틴
                    }
                    else
                    {
                        if (checkBox_nodata.Checked)
                            Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "  No Data!!");
                    }
                }
                else
                {
                    Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "  " + "SPAM!! Delete record SQ" + get_data.sq + " : " + get_data.Subject );
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " Exception : " +ex.Message);
            }
        }

        private Alert_Data Get_Data()
        {
            SqlConnection con = new SqlConnection(CONNSTR);
            Alert_Data get_data = new Alert_Data();
            try
            {
                con.Open();
                String query = QUERY_ALERT_DATA;
                if (!QuickQueryCheck.Checked)
                {
                    query = QUERY_ALERT_DATA_Except_QuickQuery;
                }

                //String query = QUERY_ALERT_DATA1;
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader rd = com.ExecuteReader();

                if (!rd.HasRows)    // 일반 이메일이 없으면 그때 SalesMail을 검색한다
                {
                    rd.Close();
                    com.Dispose();
                    query = QUERY_ALERT_DATA_SalesMail;
                    com = new SqlCommand(query, con);
                    rd = com.ExecuteReader();
                }
                while (rd.Read())
                {
                    Employee temp_employee = new Employee();

                    get_data.sq = (int)rd["sq"];
                    get_data.Regist_System = (String)rd["Regist_System"];
                    get_data.Regist_Type = (String)rd["Regist_Type"];
                    get_data.From_sq =rd["From_sq"].ToString();
                    temp_employee = Get_Employee(get_data.From_sq);
                    get_data.From_Kor_Name = temp_employee.Kor_Name;
                    get_data.From_Eng_Name = temp_employee.Eng_Name;
                    get_data.From_Email = temp_employee.Email;
                    get_data.From_Mobile = temp_employee.Mobile;

                    if (rd["customer_sq"] != DBNull.Value)
                        get_data.customer_sq = (int)rd["customer_sq"];
                    else
                        get_data.customer_sq = 1;

                    if (rd["to_email"] != DBNull.Value)
                    {
                        get_data.To_Email = rd["to_email"].ToString().Trim() + "|";
                        get_data.To_Kor_Name = rd["to_email"].ToString().Trim() + "|";
                        get_data.To_Eng_Name = rd["to_email"].ToString().Trim() + "|";
                    }
                    else
                    {
                        get_data.To_Email = "";
                        get_data.To_Kor_Name = "";
                        get_data.To_Eng_Name = "";
                        //get_data.To_Kor_Name = get_data.To_Kor_Name + temp_employee.Kor_Name + "|";
                        //get_data.To_Eng_Name = get_data.To_Eng_Name + temp_employee.Eng_Name + "|";
                    }

                    if (rd["To_sq"] != DBNull.Value)
                    {
                        String[] tmp_to_sq;
                        tmp_to_sq = rd["To_sq"].ToString().Split('|');
                        for (int i = 0; i < tmp_to_sq.Length; i++)
                        {
                            if (tmp_to_sq[i].ToString() != "")
                            {
                                get_data.To_sq = get_data.To_sq + tmp_to_sq[i] + "|";
                                temp_employee = Get_Employee(tmp_to_sq[i]);
                                get_data.To_Kor_Name = get_data.To_Kor_Name + temp_employee.Kor_Name + "|";
                                get_data.To_Eng_Name = get_data.To_Eng_Name + temp_employee.Eng_Name + "|";
                                get_data.To_Email = get_data.To_Email + temp_employee.Email + "|";
                                get_data.To_Mobile = get_data.To_Mobile + temp_employee.Mobile + "|";
                            }

                        }
                    }

                    if (rd["CC_sq"] != DBNull.Value)
                    {
                        String[] tmp_cc_sq;
                        tmp_cc_sq = rd["CC_sq"].ToString().Split('|');
                        for (int i = 0; i < tmp_cc_sq.Length; i++)
                        {
                            if (tmp_cc_sq[i].ToString() != "")
                            {
                                get_data.CC_sq = get_data.CC_sq + tmp_cc_sq[i].ToString() + "|";
                                temp_employee = Get_Employee(tmp_cc_sq[i]);
                                get_data.CC_Kor_Name = get_data.CC_Kor_Name + temp_employee.Kor_Name + "|";
                                get_data.CC_Eng_Name = get_data.CC_Eng_Name + temp_employee.Eng_Name + "|";
                                get_data.CC_Email = get_data.CC_Email + temp_employee.Email + "|";
                                get_data.CC_Mobile = get_data.CC_Mobile + temp_employee.Mobile + "|";
                            }
                        }
                    }

                    if (rd["BCC_sq"] != DBNull.Value)
                    {
                        String[] tmp_bcc_sq;
                        tmp_bcc_sq = rd["BCC_sq"].ToString().Split('|');
                        for (int i = 0; i < tmp_bcc_sq.Length; i++)
                        {
                            if (tmp_bcc_sq[i].ToString() != "")
                            {
                                get_data.BCC_sq = tmp_bcc_sq[i].ToString();
                                temp_employee = Get_Employee(tmp_bcc_sq[i]);
                                get_data.BCC_Kor_Name = get_data.BCC_Kor_Name + temp_employee.Kor_Name + "|";
                                get_data.BCC_Eng_Name = get_data.BCC_Eng_Name + temp_employee.Eng_Name + "|";
                                get_data.BCC_Email = get_data.BCC_Email + temp_employee.Email + "|";
                                get_data.BCC_Mobile = get_data.BCC_Mobile + temp_employee.Mobile + "|";
                            }
                        }
                    }
                    get_data.Subject = (String)rd["Subject"];

                    // 20180520 goodbug 스팸처리

                    if (get_data.Subject.Contains("obdvpyeu"))
                    {
                        get_data.spam = true;
                    }
                    else if (get_data.Subject.Contains("cooetpim"))
                    {
                        get_data.spam = true;
                    }
                    else if (get_data.Subject.Contains("ibrunykl"))
                    {
                        get_data.spam = true;
                    }
                    else if (get_data.Subject.Contains("iush"))
                    {
                        get_data.spam = true;
                    }
                    else if (get_data.Subject.Contains("aqhruuro"))
                    {
                        get_data.spam = true;
                    }

                    if (get_data.spam)
                    {
                        delete_alert(get_data.sq);
                    }

                    get_data.Body = (String)rd["Body"];
                    get_data.Method = (String)rd["Method"];
                    get_data.HTML = (String)rd["HTML"];
                    get_data.Priority = (String)rd["Priority"];
                    get_data.Regist_Datetime = (DateTime)rd["Regist_Datetime"];
                    get_data.Send_Datetime = (DateTime)rd["Send_Datetime"];
                    if (rd["Sent_Datetime"] != DBNull.Value)
                    {
                        get_data.Regist_Datetime = (DateTime)rd["Sent_Datetime"];
                    }
                    if (rd["smtp_id"] != DBNull.Value)
                    {
                        get_data.smtp_id = (String)rd["smtp_id"];
                    }
                    else
                    {
                        get_data.smtp_id = "";
                    }

                    if (rd["smtp_pw"] != DBNull.Value)
                    {
                        get_data.smtp_pw = (String)rd["smtp_pw"];
                    }
                    else
                    {
                        get_data.smtp_pw = "";
                    }

                }

                rd.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " Exception : " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();

                }
            }
            return get_data;

        }

        private Employee Get_Employee(string sq)
        {
            SqlConnection con = new SqlConnection(CONNSTR);
            Employee get_data = new Employee();
            try
            {
                con.Open();
                String query = QUERY_EMPLOYEE_DATA + sq;
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader rd = com.ExecuteReader();
                
                while (rd.Read())
                {
                    get_data.Kor_Name = "";
                    get_data.Eng_Name = "";
                    get_data.Email = "";
                    get_data.Mobile = "";

                    get_data.sq = (int)rd["sq"];
                    if (rd["Kor_Name"] != DBNull.Value)
                        get_data.Kor_Name = (String)rd["Kor_Name"];
                    if (rd["Eng_Name"] != DBNull.Value)
                    get_data.Eng_Name = (String)rd["Eng_Name"];
                    if (rd["Email"] != DBNull.Value)
                    get_data.Email = (String)rd["Email"];
                    if (rd["Mobile_phone"] != DBNull.Value)
                    get_data.Mobile = (String)rd["Mobile_phone"];
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " Exception : " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    
                }
            }
            return get_data;
        }

        private int update_alert(int sq)
        {
             SqlConnection con = new SqlConnection(CONNSTR);
             try
             {

                 con.Open();
                 SqlCommand cmd = new SqlCommand();

                 cmd.Connection = con;

                 cmd.CommandText = @"update alert set sent_datetime = getdate() ";
                 cmd.CommandText = cmd.CommandText + " where sq = " + sq.ToString();

                 cmd.ExecuteNonQuery();
             }
            catch (Exception ex)
             {
                 Debug.WriteLine(ex.Message);
                 Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " Exception : " + ex.Message);
             }
             finally
             {
                 if (con != null)
                 {
                     con.Close();
                 }
             }
             return 1;

        }


        private int delete_alert(int sq)
        {
            SqlConnection con = new SqlConnection(CONNSTR);
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                cmd.CommandText = @"delete from alert where sq = " + sq.ToString();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log_Write(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " Exception : " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return 1;

        }


        private void Log_Write(string param)
        {
            //textBox_log.Text = textBox_log.Text + param;
            string trans_str = param.Replace("\n", "").Replace("\r", "");
            try
            {
                AppendText(trans_str, Color.Black);
                richTextBox_log.ScrollToCaret();

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Log_Write(ex.Message);
            }
            finally
            {
                richTextBox_log.Refresh();
                if (richTextBox_log.Lines.Count() > 1000)
                {
                    richTextBox_log.Clear();
                }
            }
        }

        private void AppendText(string text, Color color)
        {
            richTextBox_log.SelectionStart = richTextBox_log.TextLength;
            richTextBox_log.SelectionLength = 0;

            richTextBox_log.SelectionColor = color;
            richTextBox_log.AppendText(text);
            richTextBox_log.AppendText("\r\n");
            richTextBox_log.SelectionColor = richTextBox_log.ForeColor;
        }
    }
}
