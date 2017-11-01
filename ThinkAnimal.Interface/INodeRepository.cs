using ThinkAnimal.Models;

namespace ThinkAnimal.Interface
{
    public interface INodeRepository
    {
        NodeModel Get(SearchNodeModel searchNodeModel);
        bool Post(NodeModel nodeModel);
    }
}
