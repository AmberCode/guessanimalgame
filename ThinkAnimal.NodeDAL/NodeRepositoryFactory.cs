using ThinkAnimal.Interface;

namespace ThinkAnimal.NodeDAL
{
    public class NodeRepositoryFactory
    {
        /// <summary>
        /// Creates Node DAO
        /// </summary>
        /// <param name="connectionString">connection string</param>
        /// <returns>Node DAO</returns>
        public static INodeRepository Create(string connectionString)
        {
            return new SqlNodeRepository(connectionString);
        }
    }
}
