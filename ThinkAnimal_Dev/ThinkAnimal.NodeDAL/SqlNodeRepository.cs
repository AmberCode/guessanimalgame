using System;
using System.Data;
using System.Data.SqlClient;
using ThinkAnimal.Interface;
using ThinkAnimal.Models;
using Shared;

namespace ThinkAnimal.NodeDAL
{
    public class SqlNodeRepository : INodeRepository
    {
        private SqlDbManager _sqlDBManager;

        public SqlNodeRepository(string connectionString)
        {
            this._sqlDBManager = new SqlDbManager(connectionString);
        }

        /// <summary>
        /// Gets node by type and parent node ID
        /// </summary>
        /// <param name="searchNodeModel">Type and parent node ID</param>
        /// <returns>Founded node or blank node</returns>
        public NodeModel Get(SearchNodeModel searchNodeModel)
        {

            NodeModel nodeModel = new NodeModel();

            try
            {
                this._sqlDBManager.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Nodes Where Type = @Type And ParentNodeId = @ParentNodeId", this._sqlDBManager.SqlConnection))
                {

                    cmd.Parameters.Add("@Type", SqlDbType.Bit).Value = searchNodeModel.Type;
                    cmd.Parameters.Add("@ParentNodeId", SqlDbType.Int).Value = searchNodeModel.ParentNodeId;

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
                //TODO: implement error handling
                //log err
            }
            finally
            {
                this._sqlDBManager.CloseConnection();
            }

            return nodeModel;
        }

        /// <summary>
        /// Creates node
        /// </summary>
        /// <param name="nodeModel">New node object</param>
        /// <returns>True if it was created otherwise false</returns>
        public bool Post(NodeModel nodeModel)
        {
            try
            {
                this._sqlDBManager.OpenConnection();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Nodes (Feature, Animal, Type, ParentNodeId) Values (@Feature, @Animal, @Type, @ParentNodeId)",
                    this._sqlDBManager.SqlConnection))
                {
                    cmd.Parameters.Add("@Feature", SqlDbType.VarChar, 500).Value = nodeModel.Feature;
                    cmd.Parameters.Add("@Animal", SqlDbType.VarChar, 100).Value = nodeModel.Animal;
                    cmd.Parameters.Add("@Type", SqlDbType.Bit).Value = nodeModel.Type;
                    cmd.Parameters.Add("@ParentNodeId", SqlDbType.Int).Value = nodeModel.ParentNodeId;

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0) return true;
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

            return false;
        }
    }
}
