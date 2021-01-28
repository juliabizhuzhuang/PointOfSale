using System.Configuration;
using System.Data;
using System.Windows.Forms;

using System.Data.SqlClient;
//

namespace WindowsFormsPOS
{
	static class SQLConn
	{
		public static string ServerName;
		public static string PortName;
		public static string UserName;
		public static string Pwd;
		public static string DBName;
		public static string sqL;
		public static DataSet ds = new DataSet();
		public static SqlCommand cmd;
		public static SqlDataReader dr;
		public static SqlDataAdapter da ;
		public static bool adding;
		public static bool updating;

		public static string strSearch = "";

		//	public static MySqlDataAdapter da = new MySqlDataAdapter();

		//public static MySqlConnection conn = new MySqlConnection();
		public static SqlConnection conn = new SqlConnection();
		public static void getData()
		{
			string AppName = Application.ProductName;

            try
            {
				DBName = ConfigurationManager.AppSettings["DB_Name"];
				ServerName = ConfigurationManager.AppSettings["DB_IP"];
                PortName = ConfigurationManager.AppSettings["DB_Port"];
                UserName = ConfigurationManager.AppSettings["DB_User"];
                Pwd = ConfigurationManager.AppSettings["DB_Password"];
			}
            catch
            {
                MessageBox.Show("System registry was not established, you can set/save " + "these settings by pressing F1");
            }

        }

		public static void ConnDB()
		{
			conn.Close();
			try {
				//POSDBConnectionString
				conn.ConnectionString = ConfigurationManager.ConnectionStrings[DBName??"POSDB"+ "ConnectionString"].ConnectionString;
					//@"Data Source="+ServerName+";Initial Catalog="+ DBName+"; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
				conn.Open();
        } 
			catch  {
				MessageBox.Show("The system failed to establish a connection","Database Settings", MessageBoxButtons.OK,MessageBoxIcon.Information);
			}

}


		public static void DisconnMy()
		{
			conn.Close();
			conn.Dispose();

		}

		public static void SaveData()
		{
			string AppName = Application.ProductName;
			AddOrUpdateAppSettings("DB_Name", DBName);
			AddOrUpdateAppSettings("DB_IP", ServerName);
			AddOrUpdateAppSettings("DB_Port", PortName);
			AddOrUpdateAppSettings("DB_User", UserName);
			AddOrUpdateAppSettings("DB_Password", Pwd);
		}
		static void AddOrUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings");
			}
		}

	}
}

