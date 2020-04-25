using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public Categoria() { }
        public Categoria(int Id, string Desc)
        {
            ID = Id;
            Descripcion = Desc;
        }
        public override string ToString()
         {

             return Descripcion.ToString();
         }
    }
}
         
