
namespace ThinkAnimal.Models
{
    public class NodeModel
    {
        public int Id { get; set; }

        public string Feature { get; set; }

        public string Animal { get; set; }

        public bool Type { get; set; }

        public int ParentNodeId { get; set; }
    }
}
