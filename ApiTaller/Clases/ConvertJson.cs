using System.Data;
using System.Text;
using System;

namespace ApiTaller.Clases
{
    public class ConvertJson
    {
        public string SerializarJson(DataTable table)
        {
            string c34 = Encoding.Default.GetString(new byte[] { 34 });
            try
            {
                var jsonString = new StringBuilder();
                jsonString.Append("{" + c34 + "results" + c34 + ":");
                if (table.Rows.Count > 0)
                {
                    jsonString.Append("[");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        jsonString.Append("{");
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j < table.Columns.Count - 1)
                            {
                                jsonString.Append(c34 + table.Columns[j].ColumnName.ToString()
                                                  + c34 + ":" + c34
                                                  + table.Rows[i][j].ToString() + c34 + ",");
                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                jsonString.Append(c34 + table.Columns[j].ColumnName.ToString()
                                                  + c34 + ":" + c34
                                                  + table.Rows[i][j].ToString() + c34);
                            }
                        }
                        if (i == table.Rows.Count - 1)
                        {
                            jsonString.Append("}");
                        }
                        else
                        {
                            jsonString.Append("},");
                        }
                    }
                    jsonString.Append("]");
                }
                jsonString.Append("}");
                return jsonString.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
