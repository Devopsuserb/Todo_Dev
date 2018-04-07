using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Todo_Dev
{
    /// <summary>
    /// Summary description for TodoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TodoService : System.Web.Services.WebService
    {
        SqlConnection ConnectingToSql = new SqlConnection(ConfigurationManager.ConnectionStrings["connectingToTODOdatabase"].ToString());
        SqlCommand SqlCmd;
        SqlDataAdapter TaskDataAdapter;
        DataSet TaskDataset;
        DataView Dataview;
        List<TaskClass> TaskList = new List<TaskClass>();
        int Count = 1;
        [WebMethod]
        public void getData()
        {
            using (ConnectingToSql)
            {
                SqlCmd = new SqlCommand("select * from tasktable", ConnectingToSql)
                {
                    CommandType = CommandType.Text
                };
                TaskDataAdapter = new SqlDataAdapter(SqlCmd);
                TaskDataset = new DataSet();
                if (TaskDataset != null)
                {
                    TaskDataAdapter.Fill(TaskDataset);
                    Dataview = new DataView(TaskDataset.Tables[0]);
                    foreach (DataRowView TaskDatarow in Dataview)
                    {
                        TaskClass TC = new TaskClass
                        {
                            TaskID = Convert.ToInt32(TaskDatarow["task_id"]),
                            TaskName = TaskDatarow["task_name"].ToString(),
                            TaskStatus = TaskDatarow["task_status"].ToString().ToUpper(),
                            Numbering = Count
                        };
                        TC.TaskButtonStatus = (TC.TaskStatus == "COMPLETED") ? "ACTIVE" : "COMPLETED";
                        TaskList.Add(TC);
                        Count++;
                    }
                    JavaScriptSerializer JS = new JavaScriptSerializer();
                    Context.Response.Write(JS.Serialize(TaskList));
                }
                else
                {
                    throw new Exception();
                }
            }
        }
  
        [WebMethod]
        public void DeleteRecord(int Task_ID)
        {
            try
            {
                SqlCmd = new SqlCommand("DeleteSingleTask", ConnectingToSql)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter Task_ID_Parameter = new SqlParameter("@TaskID", SqlDbType.Int)
                {
                    Value = Task_ID
                };
                SqlCmd.Parameters.Add(Task_ID_Parameter);
                ConnectingToSql.Open();
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectingToSql.Close();
            }
        }
        [WebMethod]
        public void UpdateRecord(string TaskStatus, int TaskID)
        {
            if (TaskStatus == "COMPLETED") TaskStatus = "ACTIVE"; else TaskStatus = "COMPLETED";
            try
            {
                SqlCmd = new SqlCommand("UpdateSingleTask", ConnectingToSql)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter TaskStatus_Parameter = new SqlParameter("@TaskStatus", SqlDbType.Char,9 )
                {
                    Value = TaskStatus
                };
                SqlParameter TaskID_Parameter = new SqlParameter("@TaskID", SqlDbType.Int)
                {
                    Value = TaskID
                };
                SqlCmd.Parameters.Add(TaskStatus_Parameter);
                SqlCmd.Parameters.Add(TaskID_Parameter);
                ConnectingToSql.Open();
                SqlCmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                ConnectingToSql.Close();
            }
        }
        [WebMethod]
        public void DeleteAllTasks()
        {
            try
            {
                SqlCmd = new SqlCommand("Delete from tasktable", ConnectingToSql)
                {
                    CommandType = CommandType.Text
                };
                ConnectingToSql.Open();
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectingToSql.Close();
            }
        }
        [WebMethod]
        public void InsertRecord(string Task_Name)
        {
            try
            {
                SqlCmd = new SqlCommand("InsertTask", ConnectingToSql)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameter TaskStatus_Parameter = new SqlParameter("@TaskName", SqlDbType.VarChar, 200)
                {
                    Value = Task_Name
                };
                SqlCmd.Parameters.Add(TaskStatus_Parameter);
                ConnectingToSql.Open();
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectingToSql.Close();
            }

        }
        [WebMethod]
        public void getDataForCharts()
        {
            ChartData Data = new ChartData();
            try
            {
                SqlCmd = new SqlCommand("GetChartData", ConnectingToSql)
                {
                    CommandType = CommandType.StoredProcedure
                };
                ConnectingToSql.Open();
                SqlCmd.ExecuteNonQuery();
                TaskDataAdapter = new SqlDataAdapter(SqlCmd);
                TaskDataset = new DataSet();
                if (TaskDataset != null)
                {
                    TaskDataAdapter.Fill(TaskDataset);
                    Dataview = new DataView(TaskDataset.Tables[0]);
                    Data.ActiveRecordCount = (Dataview[0] != null) ? Convert.ToInt32(Dataview[0]["CountOfRecords"]) : 0;
                    Data.CompletedRecordCount = (Dataview[1] != null) ? Convert.ToInt32(Dataview[1]["CountOfRecords"]) : 0;

                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
            }
            finally
            {
                ConnectingToSql.Close();
            }
            JavaScriptSerializer JS = new JavaScriptSerializer();
            Context.Response.Write(JS.Serialize(Data));
        }
    }
}