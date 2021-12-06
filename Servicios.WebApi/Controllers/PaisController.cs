using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Servicios.Paises.Models;

namespace Servicios.WebApi.Controllers
{
    public class PaisController : ApiController
    {
        Negocios2021Entities2 db = new Negocios2021Entities2();

        public IEnumerable<paises> Get()
        {
            var listado = db.paises.ToList();
            return listado;
        }
    }
}
