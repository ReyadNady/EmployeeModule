using System;
using System.Data;

namespace Admins_Transportation.helper
{
    public class DataTableMethodsClass
    {
        #region Members

        private static DataTableMethodsClass instance;

        #endregion Members

        #region Constructor

        private DataTableMethodsClass()
        {
        }

        #endregion Constructor

        #region Funcations

        #region GetInstance

        public static DataTableMethodsClass Getinstance()
        {
            if (instance == null)
                instance = new DataTableMethodsClass();
            return instance;
        }

        #endregion GetInstance

        #region Convert DataTable To HTML

        public string ConvertDataTableToHTML(DataTable dt)
        {
            string messageBody = "<font>The following are the records: </font><br><br>";
            if (dt == null || dt.Rows.Count == 0) return messageBody;
            string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
            string htmlTableEnd = "</table>";
            string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
            string htmlHeaderRowEnd = "</tr>";
            string htmlTrStart = "<tr style=\"color:#555555;\">";
            string htmlTrEnd = "</tr>";
            string htmlTdStart = "<td style=\" border-color:#fcc630; border-style:solid; border-width:thin; padding: 5px;\">";
            string htmlTdEnd = "</td>";
            messageBody += htmlTableStart;
            messageBody += htmlHeaderRowStart;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                messageBody += htmlTdStart + dt.Columns[i].ColumnName + htmlTdEnd;
            }
            messageBody += htmlHeaderRowEnd;
            //Loop all the rows from grid vew and added to html td
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                messageBody += htmlTrStart;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    messageBody = messageBody + htmlTdStart + dt.Rows[i][j].ToString() + htmlTdEnd;
                }
                messageBody += htmlTrEnd;
            }
            messageBody += htmlTableEnd;
            return messageBody; // return HTML Table as string from this function
        }

        #endregion Convert DataTable To HTML

        #region Get Total Pages  DataTable

        public int GetTotalPagesDataTable(DataTable dtData)
        {
            int totalPages = 1;
            if (dtData != null && dtData.Rows.Count > 0)
            {
                int totalRows = Convert.ToInt32(dtData.Rows[0]["count"].ToString());
                if (totalRows > 0)
                    return (int)Math.Ceiling((double)totalRows / ConstansValuesClass.PageSize);
                else
                    return totalPages;
            }
            return totalPages;
        }

        #endregion Get Total Pages  DataTable

        #region cast Dialogue Items  DataTable

        public DataTable CastDialogueItemsWithAll(DataTable dt, string columnId, string columnName, string All, bool statusWithAll)
        {
            DataTable dtNewData = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                dtNewData.Columns.Add("itemId", typeof(int));
                dtNewData.Columns.Add("itemPostion", typeof(int));
                dtNewData.Columns.Add("itemName", typeof(string));
                int postion = 0;
                if (statusWithAll == true)
                {
                    dtNewData.Rows.Add(0, postion, All);
                    postion += 1;
                }
                //
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtNewData.Rows.Add(dt.Rows[i][columnId].ToString(),
                    postion,
                dt.Rows[i][columnName + ""].ToString().Trim());
                    postion = postion + 1;
                }
            }
            return dtNewData;
        }

        #endregion cast Dialogue Items  DataTable

        #region Get cout of Items DataTable

        public int GetTotalRows(DataTable dtData)
        {
            int totalItems = 0;
            if (dtData != null && dtData.Rows.Count > 0)
                return Convert.ToInt32(dtData.Rows[0]["count"].ToString());

            return totalItems;
        }

        #endregion Get cout of Items DataTable

        #region Compare two DataTables and return a DataTable with DifferentRecords

        public DataTable GetDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            //Create Empty Table
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object
            using (DataSet ds = new DataSet())
            {
                //Add tables
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }

        #endregion Compare two DataTables and return a DataTable with DifferentRecords

        #region Ceck Vlaue in DataTable

        public DataRow[] CheckVlaueInDataTable(DataTable dt, string columName, string searchAuthor)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] foundAuthors = dt.Select("" + columName + " = '" + searchAuthor + "'");
                if (foundAuthors.Length != 0)
                    return foundAuthors;
                return new DataRow[] { };
            }
            return new DataRow[] { };
        }

        #endregion Ceck Vlaue in DataTable

        #endregion Funcations
    }

}
