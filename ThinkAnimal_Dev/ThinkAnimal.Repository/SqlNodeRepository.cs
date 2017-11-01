using System;
using System.Data.SqlClient;
using ThinkAnimal.Interface;
using ThinkAnimal.Models;

namespace ThinkAnimal.Repository
{
    public class SqlNodeRepository : INodeRepository
    {
        private SqlDbManager _sqlDBManager;

        public SqlNodeRepository(string connectionString)
        {
            this._sqlDBManager = new SqlDbManager(connectionString); 
        }

        public NodeModel Get(SearchNodeModel searchNodeModel)
        {

            NodeModel nodeModel = new NodeModel();

            try
            {
                this._sqlDBManager.OpenConnection();

                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Nodes Where Type = '{0}' And ParentNodeId = '{1}'", searchNodeModel.Type, searchNodeModel.ParentNodeId), this._sqlDBManager.SqlConnection))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            nodeModel.Id = Convert.ToInt32(dataReader["Id"]);
                            nodeModel.Feature = dataReader["Feature"].ToString();
                            nodeModel.Animal = dataReader["Animal"].ToString();
                            nodeModel.Type = Convert.ToBoolean(dataReader["Type"]);
                            nodeModel.ParentNodeId = Convert.ToInt32(dataReader["ParentNodeId"]);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                //log err
            }
            finally
            {
                this._sqlDBManager.CloseConnection();
            }

            return nodeModel;
        }

    }
}
