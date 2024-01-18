using CLI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class IndeksController
    {
        private readonly IndeksDAO indeksDAO;

        public IndeksController()
        {
            indeksDAO = new IndeksDAO();  
        }

    }
}
