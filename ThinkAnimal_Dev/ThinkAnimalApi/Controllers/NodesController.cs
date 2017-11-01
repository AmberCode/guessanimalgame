using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Configuration;

using ThinkAnimal.Interface;
using ThinkAnimal.Models;
using ThinkAnimal.NodeDAL;


namespace ThinkAnimal.Controllers
{
    public class NodesController : ApiController
    {
        private INodeRepository _nodeRepository;

        public NodesController(INodeRepository nodeRepository)
        {
            this._nodeRepository = nodeRepository;
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] SearchNodeModel search)   
        {
            var node = this._nodeRepository.Get(search);

            return Ok(node);
        }

        [HttpPost]
        public bool Post([FromUri] NodeModel nodeModel)
        {
            return this._nodeRepository.Post(nodeModel);
        }
    }
}
