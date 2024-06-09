using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_API.DbConnection;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
