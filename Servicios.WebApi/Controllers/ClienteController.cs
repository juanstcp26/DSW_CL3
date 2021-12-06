using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Servicios.Data.Models;

namespace Servicios.WebApi.Controllers
{
    public class ClienteController : ApiController
    {
        Negocios2021Entities db = new Negocios2021Entities();

        public IEnumerable<clientes> Get()
        {
            var listado = db.clientes.ToList();
            return listado;
        }

        [HttpGet]
        public clientes Get(string cod)
        {
            var objemp = db.clientes.FirstOrDefault(x => x.IdCliente == cod);
            return objemp;
        }

        [HttpPost]
        public bool Post(clientes cli)
        {
            db.clientes.Add(cli);
            return db.SaveChanges() > 0;
        }

        [HttpPut]
        public bool Put(clientes emp)
        {
            var clientes = db.clientes.FirstOrDefault(x => x.IdCliente == emp.IdCliente);
            clientes.NomCliente = emp.NomCliente;
            clientes.DirCliente = emp.DirCliente;
            clientes.idpais = emp.idpais;
            clientes.fonoCliente = emp.fonoCliente;
            return db.SaveChanges() > 0;
        }

        [HttpDelete]
        public bool Delete(string cod)
        {
            var clientes = db.clientes.FirstOrDefault(x => x.IdCliente == cod);
            db.clientes.Remove(clientes);
            return db.SaveChanges() > 0;
        }
    }
}
